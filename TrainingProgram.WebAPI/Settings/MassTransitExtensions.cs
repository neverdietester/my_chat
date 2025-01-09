using MassTransit;
using Trainingprogram.Contracts.Shared.Messages;

namespace TrainingProgram.WebAPI.Settings
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection InstallMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
