using CourseManager.WebAPI.Interfaces;
using Trainingprogram.RepositoriesAbstractions.Courses.AnswerRepository;
using Trainingprogram.RepositoriesAbstractions.Courses.CourseRepository;
using Trainingprogram.RepositoriesAbstractions.Courses.LessonRepository;
using Trainingprogram.RepositoriesAbstractions.QuestionRepository;
using Trainingprogram.Services.Abstractions.CourseManager;
using TrainingProgram.Entities.Settings;
using TrainingProgram.Infrastructure.MongoCourse;
using TrainingProgram.Infrastructure.MongoCourse.Infrastructure.Repositories.Implementations.CourseManager;
using TrainingProgram.Services.CourseManagerService;
namespace CourseManager.WebAPI
{
    public static class Registrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton((IConfigurationRoot)configuration)
                    .InstallServices()
                    .ConfigureContextMongo(configuration)
                    .Configure<JwtSettings>(configuration.GetSection(JwtSettings.DefaultSection))
                    .InstallRepositories();
            return services;
        }
        private static IServiceCollection InstallServices(this IServiceCollection serviceCollection)
        {

            serviceCollection
            .AddTransient<ICourseService, CourseService>()
            .AddTransient<ILessonService, LessonService>()
            .AddTransient<IAnswerService, AnswerService>()
            .AddTransient<IQuestionService, QuestionService>()
            .AddTransient<IVideoService, VideoService>()
            .AddHttpContextAccessor();
            return serviceCollection;
        }
        private static IServiceCollection InstallRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<ICourseRepository, CourseRepository>()
                .AddTransient<ILessonRepository, LessonRepository>()
                .AddTransient<IAnswerRepository, AnswerRepository>()
                .AddTransient<IQuestionRepository, QuestionRepository>();
            return serviceCollection;
        }
    }
}
