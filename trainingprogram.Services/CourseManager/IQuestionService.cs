using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingprogram.Contracts.CourseManager.Question;
using Trainingprogram.RepositoriesAbstractions.QuestionRepository;
using TrainingProgram.Entities.Result;

namespace Trainingprogram.Services.Abstractions.CourseManager
{
    public interface IQuestionService
    {

        public Task<BaseResult<QuestionDTO>> AddQuestionToLesson(ObjectId lessonId, QuestionDTO questionDto);

        public Task<BaseResult<QuestionDTO>> GetQuestion(ObjectId questionId);

        public Task<BaseResult<List<QuestionDTO>>> GetQuestionsByLesson(ObjectId lessonId);

        public Task<BaseResult<bool>> RemoveQuestionFromLesson(ObjectId lessonId, ObjectId questionId);

        public Task<BaseResult<QuestionDTO>> UpdateQuestion(ObjectId questionId, QuestionUpdateDTO questionDto);
    }
}
