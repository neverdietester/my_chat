using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingprogram.Contracts.CourseManager.Course;
using TrainingProgram.Entities.CourseEntities;
using MassTransit.Mediator;
using MediatR;
using Trainingprogram.Services.Abstractions.CourseManager;
using TrainingProgram.Services.CourseManagerService;
using MassTransit;
using RabbitMQ.Messages;
using ZstdSharp.Unsafe;
using TrainingProgram.Entities.Result;
using Trainingprogram.Contracts.CourseManager.Lesson;

namespace RabbitMQ
{
    public class CreateCourseCommand : IRequest<CourseResponseDTO>
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public List<LessonCreateDTO> Lessons { get; set; }
    }

    public class CreateCourseHandler : IRequestHandler<CreateCourseCommand, CourseResponseDTO>
    {
        private readonly ICourseService _courseService;

        public CreateCourseHandler(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<CourseResponseDTO> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            return new CourseResponseDTO
            {
                Name = request.Name,
                AuthorId = request.AuthorId.ToString(),
                Description = request.Description
            };

        }
    }
}