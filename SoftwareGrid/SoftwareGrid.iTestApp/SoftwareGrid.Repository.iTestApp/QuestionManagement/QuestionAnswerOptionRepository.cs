using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using SoftwareGrid.Model.iTestApp.QuestionManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.QuestionManagement
{
    public class QuestionAnswerOptionRepository : BaseRepository<QuestionAnswerOption>, IQuestionAnswerOptionRepository
    {
        private readonly AppDbContext _dbContext;
        public QuestionAnswerOptionRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<QuestionAnswerOption> GetByQuestionId(int questionId, bool withCorrectAnswerOption = false)
        {
            string query;
            if (!withCorrectAnswerOption)
            {
                query =
                    @"SELECT QAO.AnswerOptionId,QAO.QuestionId,QAO.AnswerOptionText,QAO.AnswerOptionImageName,0,QAO.CreatedDate,QAO.CreatedByUserId
                         FROM  QuestionAnswerOption QAO WHERE QAO.QuestionId = @QuestionId";
            }
            else
            {
                query =
                   @"SELECT QAO.AnswerOptionId,QAO.QuestionId,QAO.AnswerOptionText,QAO.AnswerOptionImageName,QAO.IsCorrectAnswer,QAO.CreatedDate,QAO.CreatedByUserId
                         FROM  QuestionAnswerOption QAO WHERE QAO.QuestionId = @QuestionId";
            }

            return _dbContext.SqlConnection.Query<QuestionAnswerOption>(query, new { QuestionId = questionId }).ToList();
        }

    }

    public interface IQuestionAnswerOptionRepository : IBaseRepository<QuestionAnswerOption>
    {
        IEnumerable<QuestionAnswerOption> GetByQuestionId(int questionId, bool withCorrectAnswerOption = false);
    }
}
