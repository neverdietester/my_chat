using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Consumers;
using Trainingprogram.Services.Abstractions.CourseManager;
using TrainingProgram.Infrastructure.MongoCourse.Infrastructure.Repositories.Implementations.CourseManager;
using TrainingProgram.Services.CourseManagerService;

namespace RabbitMQ
{
    public static class MassTransitConfig
    {
        public static void AddMassTransitConfiguration(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CourseCreateEventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    // Регистрируем все endpoints
                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}

