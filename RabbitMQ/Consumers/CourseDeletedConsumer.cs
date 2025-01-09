using MassTransit;
using Trainingprogram.Contracts.Shared.Messages;

namespace RabbitMQ.Consumers
{
    public class CourseDeletedConsumer : IConsumer<CourseDeleted>
    {
        public async Task Consume(ConsumeContext<CourseDeleted> context)
        {
            var message = context.Message;
            await Task.CompletedTask;
        }
    }
}
