using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MassTransit;
using MongoDB.Bson;
using TrainingProgram.Infrastructure.PostgresChat;
using TrainingProgram.Services.OAuth.mapping;

namespace TrainingProgram.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
           
            // Регистрация DbContext
            services.AddDbContext<DbContextPostgressChat>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            // Добавление других сервисов, контроллеров и Swagger
            services.AddServices(Configuration);
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                options.MapType<ObjectId>(() => new OpenApiSchema { Type = "string", Format = "string" });
                //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                //{
                //    In = ParameterLocation.Header,
                //    Description = "Введите пожалуйста валидный токен",
                //    Name = "Авторизация",
                //    Type = SecuritySchemeType.Http,
                //    BearerFormat = "JWT",
                //    Scheme = "Bearer"
                //});
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        }, Array.Empty<string>()
                    }
                });
            });

            // Настройка MassTransit
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

            //// Настройка аутентификации и авторизации
            //AddAuthenticationAndAuthorization(services);

            // Настройка AutoMapper
            InstallAutomapper(services);
        }

        private static IServiceCollection InstallAutomapper(IServiceCollection services)
        {
            services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));
            return services;
        }

        private static MapperConfiguration GetMapperConfiguration()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserMapper>();
            });
            configuration.AssertConfigurationIsValid();
            return configuration;
        }

        //public void AddAuthenticationAndAuthorization(IServiceCollection services)
        //{
        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //    }).AddJwtBearer(o =>
        //    {
        //        var options = Configuration.GetSection(JwtSettings.DefaultSection).Get<JwtSettings>();
        //        var jwtKey = options.JwtKey;
        //        var issuer = options.Issuer;
        //        var audience = options.Audience;

        //        o.Authority = options.Authority;
        //        o.RequireHttpsMetadata = false;
        //        o.TokenValidationParameters = new TokenValidationParameters()
        //        {
        //            ValidIssuer = issuer,
        //            ValidAudience = audience,
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        //            ValidateAudience = true,
        //            ValidateIssuer = true,
        //            ValidateLifetime = true,
        //            ValidateIssuerSigningKey = true
        //        };
        //    });
        //}

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            if (!env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}