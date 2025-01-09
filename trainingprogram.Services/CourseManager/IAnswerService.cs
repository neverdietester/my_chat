using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingprogram.Contracts.CourseManager.Answer;
using TrainingProgram.Entities.Result;

namespace Trainingprogram.Services.Abstractions.CourseManager
{
    public interface IAnswerService
    {
        public Task<BaseResult<AnswerDTO>> AddAnswerToQuestion(ObjectId questionId, AnswerDTO answerDto);

        public Task<BaseResult<List<AnswerDTO>>> GetAnswersByQuestion(ObjectId questionId);

        public Task<BaseResult<AnswerDTO>> GetAnswer(ObjectId answerId);

        public Task<BaseResult<AnswerDTO>> UpdateAnswer(ObjectId answerId, AnswerUpdateDTO answerDto);

        public Task<BaseResult<bool>> RemoveAnswer(ObjectId questionId, ObjectId answerId);

    }
}
