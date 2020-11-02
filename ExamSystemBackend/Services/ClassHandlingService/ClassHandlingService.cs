using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamSystemBackend.Exceptions;
using ExamSystemBackend.Models;

namespace ExamSystemBackend.Services.ClassHandlingService
{
    public class ClassHandlingService : IClassHandlerService
    {
        private Dictionary<string, Class> _classes = new Dictionary<string, Class>();
        
        public async Task<Class> AddClass(string teacherId, Class @class)
        {
            // TODO verify is really a teacher
            // TODO replace with actual teacher
            @class.Teachers.Add(new Participant
            {
                Id = teacherId,
                Type = ParticipantType.Teacher
            });
            
            _classes.Add(@class.Id, @class);
            return @class;
        }

        public async Task<Class> GetClass(string classId)
        {
            // TODO fetch real class
            if (!_classes.ContainsKey(classId)) return null;

            return _classes[classId];
        }

        public async Task<Class> ModifyClassState(string teacherId, string classId, ClassState state)
        {
            // TODO fetch real teacher
            // TODO fetch real class
            if (!_classes.ContainsKey(classId)) throw new ClassNotFoundException();

            bool teacherExists = false;
            Class @class = _classes[classId];
            foreach (var teacher in @class.Teachers)
            {
                if (teacher.Id == teacherId)
                {
                    teacherExists = true;
                    break;
                }
            }

            if (teacherExists)
            {
                @class.State = state;
            }

            return @class;
        }

        public async Task<Participant> AddParticipant(Participant participant, string classId)
        {
            if (participant == null) return null;
            if (string.IsNullOrEmpty(classId)) throw new ClassNotFoundException();
            
            // TODO fetch real class
            if (!_classes.ContainsKey(classId)) throw new ClassNotFoundException();

            Class @class = _classes[classId];
            if (@class.State == ClassState.Finished) throw new ClassFinishedException();

            if (participant.Type == ParticipantType.Student)
            {
                @class.Students.Add(participant);
                participant.Classes.Add(@class);
            } else if (participant.Type == ParticipantType.Teacher)
            {
                @class.Teachers.Add(participant);
                participant.Classes.Add(@class);
            }
            else
            {
                throw new InvalidOperationException();
            }

            return participant;
        }

        public async Task<List<Participant>> GetParticipants(string classId)
        {
            throw new System.NotImplementedException();
        }

        public async Task AddExamReport(ExamReport examReport)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ExamReport> GetExamReport(string participantId, string examReportId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<ExamReport>> GetExamReports()
        {
            throw new System.NotImplementedException();
        }
    }
}