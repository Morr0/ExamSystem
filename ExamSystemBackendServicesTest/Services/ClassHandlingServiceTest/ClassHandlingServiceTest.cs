using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamSystemBackend.Exceptions;
using ExamSystemBackend.Models;
using ExamSystemBackend.Services.ClassHandlingService;
using Xunit;

namespace ExamSystemBackendServicesTest.Services.ClassHandlingServiceTest
{
    public class ClassHandlingServiceTest
    {
        IClassHandlerService classHandlerService = new ClassHandlingService();
        
        [Fact]
        public async Task AddParticipantOneClassTest()
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
            Participant student2 = new Participant
            {
                Type = ParticipantType.Student
            };
            Participant teacher = new Participant
            {
                Type = ParticipantType.Teacher
            };

            // Act I
            @class = await classHandlerService.AddClass(teacher.Id, @class);

            // Assert I
            Func<Task<Participant>> action1 = async () => await classHandlerService.AddParticipant(student1, null);
            await Assert.ThrowsAsync<ClassNotFoundException>(action1);
            
            // Act II
            student1 = await classHandlerService.AddParticipant(student1, @class.Id);
            @class = await classHandlerService.GetClass(@class.Id);
            
            // Assert II
            Assert.Single(student1.Classes);
            Assert.Single(@class.Students);

            // Act III
            @class = await classHandlerService.ModifyClassState(teacher.Id, @class.Id, ClassState.Finished);
            
            // Assert III
            Assert.Equal(ClassState.Finished, @class.State);
            Func<Task<Participant>> action3 = async () => await classHandlerService.AddParticipant(student2, @class.Id);
            await Assert.ThrowsAsync<ClassFinishedException>(action3);
        }
    }
}