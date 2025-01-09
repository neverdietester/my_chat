using MassTransit;
using MongoDB.Bson;
using RabbitMQ;
using Trainingprogram.Contracts.CourseManager.Course;
using Trainingprogram.Contracts.Shared.Messages;
using TrainingProgram.Entities.CourseEntities;
using TrainingProgram.Entities.Result;


namespace TrainingProgram.WebAPI.Models
{
    public class CourseCommandPublisher : ICourseCommandPublisher
    {
        private readonly IBus bus;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public CourseCommandPublisher(IPublishEndpoint publishEndpoint, IRequestClient<CourseResponseDTO> requestClient, ISendEndpointProvider sendEndpointProvider, IBus bus)
        {
            _publishEndpoint = publishEndpoint;
            _sendEndpointProvider = sendEndpointProvider;
            this.bus = bus;
        }

        public async Task<BaseResult<DeleteCourseDto>> DeleteCourseCommand(DeleteCourseDto courseId, CancellationToken cancellationToken)
        {
            try
            {
                var bussControl = Bus.Factory.CreateUsingRabbitMq();
                var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                await bussControl.StartAsync(source.Token);
                var client = bussControl.CreateRequestClient<DeleteCourseDto>(new Uri("exchange:delete-course-queue"));
                Console.WriteLine("отправлен реквест");

                var response = await client.GetResponse<DeleteCourseDto>(courseId, cancellationToken);
                return new BaseResult<DeleteCourseDto>
                {
                    Data = response.Message
                };
            }
            catch (Exception ex)
            {
                return new BaseResult<DeleteCourseDto>
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task PublishCreateCourseCommand(CourseCreateDTO command)
        {
            await _publishEndpoint.Publish(command);
        }
        public async Task<BaseResult<ConsumerCourseDto>> PublishGetCourseCommand(string courseId, CancellationToken cancellationToken)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq();
            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            await busControl.StartAsync(source.Token);
            var client = busControl.CreateRequestClient<ConsumerCourseDto>(new Uri("exchange:get-course-queue"));
            Console.WriteLine("Requesting order status");
            var response = await client.GetResponse<ConsumerCourseDto>(new ConsumerCourseDto()
            {
                Id = courseId
            });

            return new BaseResult<ConsumerCourseDto>
            {
                Data = response.Message
            };
        }

        public async Task<BaseResult<CourseAuthorListResponseDTO>> PublishGetCourseFromAuthorCommand(string authorId, CancellationToken cancellationToken)
        {
            try
            {
                var bussControl = Bus.Factory.CreateUsingRabbitMq();
                var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                await bussControl.StartAsync(source.Token);
                var client = bussControl.CreateRequestClient<GetCourseRequest>(new Uri("exchange:get-fromAuthor-queue"));
                Console.WriteLine("отправлен реквест");

                var response = await client.GetResponse<CourseAuthorListResponseDTO>(new GetCourseRequest()
                {
                    AuthorId = authorId
                });
                return new BaseResult<CourseAuthorListResponseDTO>
                {
                    Data = response.Message
                };
            }
            catch (Exception ex)
            {
                return new BaseResult<CourseAuthorListResponseDTO>
                {
                    ErrorMessage = ex.Message
                };
            }
        }

    }
}
