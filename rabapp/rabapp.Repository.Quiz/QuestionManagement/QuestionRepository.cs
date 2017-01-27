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
    public class QuestionRepository : BaseRepository<QuestionViewModel>, IQuestionRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IQuestionAnswerOptionRepository _iQuestionAnswerOptionRepository;
        public QuestionRepository(
            IQuestionAnswerOptionRepository iQuestionAnswerOptionRepository
            , AppDbContext appDbContext)
            : base(appDbContext)
        {
            _iQuestionAnswerOptionRepository = iQuestionAnswerOptionRepository;
            _appDbContext = appDbContext;
        }

        public IEnumerable<QuestionViewModel> Search(string keyword, int currentPage = 0, int take = 50)
        {
            int skip = Convert.ToInt32(currentPage * take);
            const string query = @"SELECT [Q].*, [QC].[QuestionCategoryName], ISNULL(COUNT(*) OVER(),0) AS TotalRecordCount 
                            FROM [dbo].[Question] [Q] 
                            INNER JOIN [dbo].[QuestionCategory] [QC] ON [QC].[QuestionCategoryId] = [Q].[QuestionCategoryId] 
                            WHERE 1 = 1 AND [Q].[CreatedByUserId] = CASE WHEN @UserId=0 THEN  [Q].[CreatedByUserId] ELSE @UserId END 
							AND (LOWER([Q].[QuestionText]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([Q].[Tags]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([Q].[Marks]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([Q].[DifficultyLevel]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([QC].[QuestionCategoryName]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([Q].[AnswerExplanation]) LIKE  '%' + LOWER(@Keyword) + '%')
                            ORDER BY [Q].[QuestionId] 
                            DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            return _appDbContext.SqlConnection.Query<QuestionViewModel>(query, new { Keyword = keyword, Skip = skip, Take = take }).ToList();
        }

        public IEnumerable<QuestionViewModel> SearchByTestId(int testId, string keyword, int currentPage = 0, int take = 50, bool mappedQuestionOnly = false, bool withAnswerOption = false, bool withCorrectAnswerOption = false)
        {
            int skip = Convert.ToInt32(currentPage * take);
            
            const string query = @"SELECT DISTINCT [Q].*, [QC].[QuestionCategoryName], [TWQ].[TestId], ISNULL(COUNT(*) OVER(),0) AS TotalRecordCount 
                            FROM [dbo].[Question] [Q]
                            LEFT JOIN [dbo].[QuestionCategory] [QC] ON [Q].[QuestionCategoryId] = [QC].[QuestionCategoryId]
                            LEFT JOIN (SELECT [TWQ].[QuestionId], [TWQ].[TestId] FROM  [dbo].[TestWiseQuestion] [TWQ] WHERE [TWQ].[TestId] = @TestId 
                            )TWQ ON [TWQ].[QuestionId] = [Q].[QuestionId]
                            WHERE 1 = 1
                            AND (@TestId = 0 OR @MappedOnly = 0 OR [TWQ].[TestId] = (CASE WHEN @MappedOnly = 1 THEN @TestId ELSE [TWQ].[TestId] END))
                            AND (LOWER([Q].[QuestionText]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([Q].[Tags]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([Q].[Marks]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([Q].[DifficultyLevel]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([QC].[QuestionCategoryName]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([Q].[AnswerExplanation]) LIKE  '%' + LOWER(@Keyword) + '%')
                            ORDER BY [Q].[QuestionId] DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            var questionList = _appDbContext.SqlConnection.Query<QuestionViewModel>(query, new { TestId = testId, Keyword = keyword, Skip = skip, Take = take, MappedOnly = mappedQuestionOnly ? 1 : 0 }).ToList();

            if (withAnswerOption)
            {
                if (questionList.Any())
                {
                    for (int i = 0; i < questionList.Count(); i++)
                    {
                        var answerOption = _iQuestionAnswerOptionRepository.GetByQuestionId(questionList.ElementAt(i).QuestionId, withCorrectAnswerOption).ToList();
                        questionList.ElementAt(i).QuestionAnswerOptionViewModelList = answerOption;
                    }
                }
            }

            return questionList;

        }

        public QuestionViewModel GetById(int questionId)
        {
            var query = @"SELECT [Q].*, [QC].[QuestionCategoryName] FROM [dbo].[Question] [Q] INNER JOIN [dbo].[QuestionCategory] [QC] ON [QC].[QuestionCategoryId] = [Q].[QuestionCategoryId] WHERE [Q].[QuestionId] = @QuestionId; SELECT [QAO].* FROM [dbo].[QuestionAnswerOption] [QAO] WHERE [QAO].[QuestionId] = @QuestionId";

            var queryMultiple = _appDbContext.SqlConnection.QueryMultiple(query, new { QuestionId = questionId });

            QuestionViewModel question = queryMultiple.Read<QuestionViewModel>().FirstOrDefault();

            if (question != null)
            {
                List<QuestionAnswerOptionViewModel> questionAnswerOptionViewModelList = queryMultiple.Read<QuestionAnswerOptionViewModel>().ToList();

                question.QuestionAnswerOptionViewModelList = questionAnswerOptionViewModelList;

                return question;
            }

            return null;
        }
    }

    public interface IQuestionRepository : IBaseRepository<QuestionViewModel>
    {
        IEnumerable<QuestionViewModel> Search(string keyword, int currentPage = 0, int take = 50);

        IEnumerable<QuestionViewModel> SearchByTestId(int testId, string keyword, int currentPage = 0, int take = 50,
            bool mappedQuestionOnly = false, bool withAnswerOption = false, bool withCorrectAnswerOption = false);

        QuestionViewModel GetById(int questionId);
    }
}
