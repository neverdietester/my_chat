using MongoDB.Bson;
using Trainingprogram.Contracts.CourseManager.Answer;
using Trainingprogram.RepositoriesAbstractions.Courses.AnswerRepository;
using Trainingprogram.RepositoriesAbstractions.QuestionRepository;
using Trainingprogram.Services.Abstractions.CourseManager;
using TrainingProgram.Entities.CourseEntities;
using TrainingProgram.Entities.Enum;
using TrainingProgram.Entities.Result;

namespace TrainingProgram.Services.CourseManagerService
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;
        public AnswerService(IAnswerRepository answerRepository, IQuestionRepository questionRepository)
        {
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
        }

        public async Task<BaseResult<AnswerDTO>> AddAnswerToQuestion(ObjectId questionId, AnswerDTO answerDto)
        {
            var question = _questionRepository.Get(questionId);
            if (question == null)
            {
                return new BaseResult<AnswerDTO>
                {
                    ErrorMessage = "Question not found",
                    ErrorCode = (int)ErrorCodes.QuestionNotFound
                };
            }

            var answer = new Answer
            {
                Id = ObjectId.GenerateNewId(),
                Text = answerDto.Text,
                IsCorrect = answerDto.IsCorrect
            };

            question.Answers.Add(answer);
            await _questionRepository.SaveChangesAsync();

            return new BaseResult<AnswerDTO>
            {
                Data = new AnswerDTO
                {
                    Id = answer.Id.ToString(),
                    Text = answer.Text,
                    IsCorrect = answer.IsCorrect
                }
            };
        }

        public async Task<BaseResult<AnswerDTO>> GetAnswer(ObjectId answerId)
        {
            var answer = _answerRepository.Get(answerId);
            if (answer == null)
            {
                return new BaseResult<AnswerDTO>
                {
                    ErrorMessage = "Answer not found",
                    ErrorCode = (int)ErrorCodes.AnswerNotFound
                };
            }

            return new BaseResult<AnswerDTO>
            {
                Data = new AnswerDTO
                {
                    Id = answer.Id.ToString(),
                    Text = answer.Text,
                    IsCorrect = answer.IsCorrect
                }
            };
        }

        public async Task<BaseResult<List<AnswerDTO>>> GetAnswersByQuestion(ObjectId questionId)
        {
            var question = _questionRepository.Get(questionId);
            if (question == null)
            {
                return new BaseResult<List<AnswerDTO>>
                {
                    ErrorMessage = "Question not found",
                    ErrorCode = (int)ErrorCodes.QuestionNotFound
                };
            }

            var answerDtos = question.Answers.Select(a => new AnswerDTO
            {
                Id = a.Id.ToString(),
                Text = a.Text,
                IsCorrect = a.IsCorrect
            }).ToList();

            return new BaseResult<List<AnswerDTO>> { Data = answerDtos };
        }

        public async Task<BaseResult<AnswerDTO>> UpdateAnswer(ObjectId answerId, AnswerUpdateDTO answerDto)
        {
            var answer = _answerRepository.Get(answerId);
            if (answer == null)
            {
                return new BaseResult<AnswerDTO>
                {
                    ErrorMessage = "Answer not found",
                    ErrorCode = (int)ErrorCodes.AnswerNotFound
                };
            }

            answer.Text = answerDto.Text;
            answer.IsCorrect = answerDto.IsCorrect;

            _answerRepository.Update(answer);
            await _answerRepository.SaveChangesAsync();

            return new BaseResult<AnswerDTO>
            {
                Data = new AnswerDTO
                {
                    Id = answer.Id.ToString(),
                    Text = answer.Text,
                    IsCorrect = answer.IsCorrect
                }
            };
        }

        public async Task<BaseResult<bool>> RemoveAnswer(ObjectId questionId, ObjectId answerId)
        {
            var question = _questionRepository.Get(questionId);
            if (question == null)
            {
                return new BaseResult<bool>
                {
                    ErrorMessage = "Question not found",
                    ErrorCode = (int)ErrorCodes.QuestionNotFound
                };
            }

            var answer = question.Answers.FirstOrDefault(a => a.Id == answerId);
            if (answer == null)
            {
                return new BaseResult<bool>
                {
                    ErrorMessage = "Answer not found in question",
                    ErrorCode = (int)ErrorCodes.AnswerNotFound
                };
            }

            question.Answers.Remove(answer);
            await _questionRepository.SaveChangesAsync();

            return new BaseResult<bool> { Data = true };
        }
    }
}
