using MassTransit;
using RabbitMQ.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingprogram.Contracts.CourseManager.Course;
using Trainingprogram.Services.Abstractions.CourseManager;

namespace RabbitMQ.Consumers
{
    public class CourseCreateEventConsumer : IConsumer<CourseCreatedEvent>
    {
        private readonly ICourseService _courseService;

        public CourseCreateEventConsumer(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task Consume(ConsumeContext<CourseCreatedEvent> context)
        {
            var courseCreatedEvent = context.Message;


            var courseCreateDTO = new CourseCreateDTO
            {
                AuthorId = courseCreatedEvent.AuthorId,
                Name =  courseCreatedEvent.Name
            };


            await _courseService.AsyncCreateCourse(courseCreateDTO);

            //await context.Publish(new CourseCreationConfirmedEvent
            //{
            //    CourseId = courseCreatedEvent.CourseId,
            //    Name = courseCreatedEvent.Name
            //});
        }
    }

}
