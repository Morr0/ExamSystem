using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamSystemBackend.Exceptions;
using ExamSystemBackend.Models;
using ExamSystemBackend.Services.ClassHandlingService;
using ExamSystemBackend.Services.ExamHandlingService;
using ExamSystemBackend.Services.ExamHandlingService.DTOs;
using Xunit;

namespace ExamSystemBackendServicesTest.Services.ExamHandlingService
{
    public class ExamHandlingServiceTest
    {
        private IExamHandlingService _examHandlingService = new ExamHandlerService();
        private IClassHandlerService _classHandlerService = new ClassHandlingService();

        [Fact]
        public async Task CreateExamAndAddToClassAndMakeSureStudentsAndTeachersViewIt()
        {
            // Arrange I
            Class @class = new Class
            {
                State = ClassState.Running
            };
            Participant student1 = new Participant
            {
                Type = ParticipantType.Student
            };
            Participant teacher = new Participant
            {
                Type = ParticipantType.Teacher
            };
            Exam exam = SampleExam(teacher);

            // Act I
            @class = await _classHandlerService.AddClass(teacher.Id, @class);
            student1 = await _classHandlerService.AddParticipant(student1, @class.Id);
            exam = await _examHandlingService.AddExam(exam);

            // Assert I
            Assert.Equal(exam.Id, (await _examHandlingService.GetExam(teacher.Id, exam.Id)).Id);

            // Act II
            ExamReport examReport = await _classHandlerService.PutExam(exam.Id);
            // Attempting questions
            foreach (var question in exam.Questions)
            {
                await _classHandlerService.RespondToQuestion(new StudentQuestionResponseDTO
                {
                    ExamReportId = examReport.Id,
                    StudentId = student1.Id,
                    QuestionId = question.Id,
                    Response = "2"
                });
            }

            int totalMark = await _classHandlerService.GetExamScore(student1.Id, examReport.Id);

            // Assert II
            Assert.Equal(exam.Id, examReport.Exam.Id);
            Assert.Equal(0, totalMark);

            // Act & Assert III
            examReport = await _classHandlerService.PutExam(exam.Id);
            // Attempting questions by a teacher which should throw
            foreach (var question in exam.Questions)
            {
                Func<Task<QuestionResponse>> action = async () =>
                    await _classHandlerService.RespondToQuestion(new StudentQuestionResponseDTO
                    {
                        ExamReportId = exam.Id,
                        StudentId = teacher.Id,
                        QuestionId = question.Id,
                        Response = "Letter"
                    });
                await Assert.ThrowsAsync<TeacherCannotRespondToQuestionException>(action);
            }

            // Act IV
            // Get full score
            await _classHandlerService.RespondToQuestion(new StudentQuestionResponseDTO
            {
                ExamReportId = examReport.Id,
                StudentId = student1.Id,
                QuestionId = "1",
                Response = "Letter"
            });
            await _classHandlerService.RespondToQuestion(new StudentQuestionResponseDTO
            {
                ExamReportId = examReport.Id,
                StudentId = student1.Id,
                QuestionId = "2",
                Response = "Vehicle"
            });
            totalMark = await _classHandlerService.GetExamScore(student1.Id, examReport.Id);
            
            // Assert IV
            Assert.Equal(2, totalMark);
        }

        [Fact]
        public async Task InputtingDifferentChoicesAsAnswers()
        {
            // Arrange I
            Class @class = new Class
            {
                State = ClassState.Running
            };
            Participant student1 = new Participant
            {
                Type = ParticipantType.Student
            };
            Participant student2 = new Participant
            {
                Type = ParticipantType.Student
            };
            Participant teacher = new Participant
            {
                Type = ParticipantType.Teacher
            };
            Exam exam = SampleExam(teacher);

            // Act
            @class = await _classHandlerService.AddClass(teacher.Id, @class);
            student1 = await _classHandlerService.AddParticipant(student1, @class.Id);
            student2 = await _classHandlerService.AddParticipant(student2, @class.Id);
            exam = await _examHandlingService.AddExam(exam);
            ExamReport examReport = await _classHandlerService.PutExam(exam.Id);
            
            await _classHandlerService.RespondToQuestion(new StudentQuestionResponseDTO
            {
                ExamReportId = examReport.Id,
                StudentId = student1.Id,
                QuestionId = "1",
                Response = "Letter"
            });
            
            // Assert
            Func<Task<QuestionResponse>> action = async () => 
                await _classHandlerService.RespondToQuestion(new StudentQuestionResponseDTO
            {
                ExamReportId = examReport.Id,
                StudentId = student1.Id,
                QuestionId = "1",
                Response = "Vehicle"
            });
            await Assert.ThrowsAsync<IncorrectAnswerChoiceException>(action);
        }

        [Fact]
        public async Task AbilityForStudentToOverrideHisAnswerWhileExamIsRunning()
        {
            // Arrange
            Class @class = new Class
            {
                State = ClassState.Running
            };
            Participant student1 = new Participant
            {
                Type = ParticipantType.Student
            };
            Participant teacher = new Participant
            {
                Type = ParticipantType.Teacher
            };
            Exam exam = SampleExam(teacher);

            // Act I
            @class = await _classHandlerService.AddClass(teacher.Id, @class);
            student1 = await _classHandlerService.AddParticipant(student1, @class.Id);
            exam = await _examHandlingService.AddExam(exam);
            ExamReport examReport = await _classHandlerService.PutExam(exam.Id);
            
            await _classHandlerService.RespondToQuestion(new StudentQuestionResponseDTO
            {
                ExamReportId = examReport.Id,
                StudentId = student1.Id,
                QuestionId = "1",
                Response = "Letter"
            });
            await _classHandlerService.RespondToQuestion(new StudentQuestionResponseDTO
            {
                ExamReportId = examReport.Id,
                StudentId = student1.Id,
                QuestionId = "2",
                Response = "Game"
            });
            await _classHandlerService.RespondToQuestion(new StudentQuestionResponseDTO
            {
                ExamReportId = examReport.Id,
                StudentId = student1.Id,
                QuestionId = "1",
                Response = "Letter"
            });
            
            // Assert I
            int score = await _classHandlerService.GetExamScore(student1.Id, examReport.Id);
            Assert.Equal(1, score);

            // Act I
            await _classHandlerService.ModifyExamState(teacher.Id, @class.Id, exam.Id, ExamReportStatus.Finished);
            
            // Assert II 
            score = await _classHandlerService.GetExamScore(student1.Id, examReport.Id);
            Assert.Equal(2, score);
        }
        
        [Fact]
        public async Task ShouldNotSubmitAfterExamFinishNorLaterNorBeforeStart()
        {
            // Arrange
            Class @class = new Class
            {
                State = ClassState.Running
            };
            Participant student1 = new Participant
            {
                Type = ParticipantType.Student
            };
            Participant teacher = new Participant
            {
                Type = ParticipantType.Teacher
            };
            Exam exam = SampleExam(teacher);

            // Act I
            @class = await _classHandlerService.AddClass(teacher.Id, @class);
            student1 = await _classHandlerService.AddParticipant(student1, @class.Id);
            exam = await _examHandlingService.AddExam(exam);
            ExamReport examReport = await _classHandlerService.PutExam(exam.Id, false);
            
            // Assert I
            Func<Task<QuestionResponse>> action = async () => 
                await _classHandlerService.RespondToQuestion(new StudentQuestionResponseDTO
            {
                ExamReportId = examReport.Id,
                StudentId = student1.Id,
                QuestionId = "1",
                Response = "Letter"
            });
            await Assert.ThrowsAsync<ExamNotRunningException>(action);
            
            // Act II
            // Scenario: emergency happened, not able to continue exam
            await _classHandlerService.ModifyExamState(teacher.Id, @class.Id, exam.Id, ExamReportStatus.Finished);
            
            // Assert II
            // And a student wants to submit
            action = async () => await _classHandlerService.RespondToQuestion(new StudentQuestionResponseDTO
            {
                ExamReportId = examReport.Id,
                StudentId = student1.Id,
                QuestionId = "1",
                Response = "Letter"
            });
            await Assert.ThrowsAsync<ExamNotRunningException>(action);
            
            // MISC scenarios not related to the previous
            // Act III
            // Scenario: Cannot submit to a marked exam
            await _classHandlerService.ModifyExamState(teacher.Id, @class.Id, exam.Id, ExamReportStatus.Marked);
            // Assert III
            action = async () => await _classHandlerService.RespondToQuestion(new StudentQuestionResponseDTO
            {
                ExamReportId = examReport.Id,
                StudentId = student1.Id,
                QuestionId = "1",
                Response = "Letter"
            });
            await Assert.ThrowsAsync<ExamNotRunningException>(action);
        }

        private Exam SampleExam(Participant author)
        {
            Question question1 = new Question
            {
                Id = "1",
                Title = "What is A?",
                Choices = new List<string>
                {
                    "Letter",
                    "Number"
                },
                CorrectAnswer = "Letter",
                TotalMark = 1
            };
            Question question2 = new Question
            {
                Id = "2",
                Title = "What is a car?",
                Choices = new List<string>
                {
                    "Vehicle",
                    "Game",
                    "Nonsense"
                },
                CorrectAnswer = "Vehicle",
                TotalMark = 1
            };
            
            Exam exam = new Exam
            {
                Name = "ExamSimple",
                Questions = new List<Question>
                {
                    question1,
                    question2
                },
                AuthorId = author.Id
            };

            return exam;
        }
    }
}