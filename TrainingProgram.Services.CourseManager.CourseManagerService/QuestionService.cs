using MongoDB.Bson;
using Trainingprogram.Contracts.CourseManager.Question;
using Trainingprogram.RepositoriesAbstractions.Courses.LessonRepository;
using Trainingprogram.RepositoriesAbstractions.QuestionRepository;
using Trainingprogram.Services.Abstractions.CourseManager;
using TrainingProgram.Entities.CourseEntities;
using TrainingProgram.Entities.Enum;
using TrainingProgram.Entities.Result;

namespace TrainingProgram.Services.CourseManagerService
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ILessonRepository _lessonRepository;

        public QuestionService(IQuestionRepository questionRepository, ILessonRepository lessonRepository)
        {
            _questionRepository = questionRepository;
            _lessonRepository = lessonRepository;
        }

        public async Task<BaseResult<QuestionDTO>> AddQuestionToLesson(ObjectId lessonId, QuestionDTO questionDto)
        {
            var lesson = _lessonRepository.Get(lessonId);
            if (lesson == null)
            {
                return new BaseResult<QuestionDTO>
                {
                    ErrorMessage = "Lesson not found",
                    ErrorCode = (int)ErrorCodes.LessonNotFound
                };
            }

            var question = new Question
            {
                Id = ObjectId.GenerateNewId(),
                Text = questionDto.Text,
                Answers = new List<Answer>()
            };

            lesson.Questions.Add(question);
            await _lessonRepository.SaveChangesAsync();

            return new BaseResult<QuestionDTO> { Data = new QuestionDTO { Id = question.Id.ToString(), Text = question.Text } };
        }

        public async Task<BaseResult<QuestionDTO>> GetQuestion(ObjectId questionId)
        {
            var question = _questionRepository.Get(questionId);
            if (question == null)
            {
                return new BaseResult<QuestionDTO>
                {
                    ErrorMessage = "Question not found",
                    ErrorCode = (int)ErrorCodes.LessonNotFound
                };
            }

            return new BaseResult<QuestionDTO> { Data = new QuestionDTO { Id = question.Id.ToString(), Text = question.Text } };
        }

        public async Task<BaseResult<List<QuestionDTO>>> GetQuestionsByLesson(ObjectId lessonId)
        {
            var lesson = _lessonRepository.Get(lessonId);
            if (lesson == null)
            {
                return new BaseResult<List<QuestionDTO>>
                {
                    ErrorMessage = "Lesson not found",
                    ErrorCode = (int)ErrorCodes.LessonNotFound
                };
            }

            var questionDtos = lesson.Questions.Select(q => new QuestionDTO { Id = q.Id.ToString(), Text = q.Text }).ToList();
            return new BaseResult<List<QuestionDTO>> { Data = questionDtos };
        }

        public async Task<BaseResult<bool>> RemoveQuestionFromLesson(ObjectId lessonId, ObjectId questionId)
        {
            var lesson = _lessonRepository.Get(lessonId);
            if (lesson == null)
            {
                return new BaseResult<bool>
                {
                    ErrorMessage = "Lesson not found",
                    ErrorCode = (int)ErrorCodes.LessonNotFound
                };
            }

            var question = lesson.Questions.FirstOrDefault(q => q.Id == questionId);
            if (question == null)
            {
                return new BaseResult<bool>
                {
                    ErrorMessage = "Question not found",
                    ErrorCode = (int)ErrorCodes.LessonNotFound
                };
            }

            lesson.Questions.Remove(question);
            await _lessonRepository.SaveChangesAsync();
            return new BaseResult<bool> { Data = true };
        }

        public async Task<BaseResult<QuestionDTO>> UpdateQuestion(ObjectId questionId, QuestionUpdateDTO questionDto)
        {
            var question = _questionRepository.Get(questionId);
            if (question == null)
            {
                return new BaseResult<QuestionDTO>
                {
                    ErrorMessage = "Question not found",
                    ErrorCode = (int)ErrorCodes.QuestionNotFound
                };
            }

            question.Text = questionDto.Text;
            _questionRepository.Update(question);
            await _questionRepository.SaveChangesAsync();

            return new BaseResult<QuestionDTO> { Data = new QuestionDTO { Id = question.Id.ToString(), Text = question.Text } };
        }
    }
}
