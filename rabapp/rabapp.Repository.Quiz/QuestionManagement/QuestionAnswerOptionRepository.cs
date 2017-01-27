using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using rabapp.Model.Quiz.QuestionManagement;
using rabapp.Model.Quiz.ViewModels;
using rabapp.Repository.Common;

namespace rabapp.Repository.Quiz.QuestionManagement
{
    public class QuestionAnswerOptionRepository : BaseRepository<QuestionAnswerOptionViewModel>, IQuestionAnswerOptionRepository
    {
        private readonly AppDbContext _appDbContext;
        public QuestionAnswerOptionRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
        }

        public IEnumerable<QuestionAnswerOptionViewModel> GetByQuestionId(int questionId, bool withCorrectAnswerOption = false)
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

            return _appDbContext.SqlConnection.Query<QuestionAnswerOptionViewModel>(query, new { QuestionId = questionId }).ToList();
        }

    }

    public interface IQuestionAnswerOptionRepository : IBaseRepository<QuestionAnswerOptionViewModel>
    {
        IEnumerable<QuestionAnswerOptionViewModel> GetByQuestionId(int questionId, bool withCorrectAnswerOption = false);
    }
}
