using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Dapper;
using SoftwareGrid.Model.iTestApp.QuestionManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.QuestionManagement
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IQuestionAnswerOptionRepository _iQuestionAnswerOptionRepository;
        public QuestionRepository(
            IQuestionAnswerOptionRepository iQuestionAnswerOptionRepository
            ,AppDbContext dbContext)
            : base(dbContext)
        {
            _iQuestionAnswerOptionRepository = iQuestionAnswerOptionRepository;
            _dbContext = dbContext;
        }
        
        public IEnumerable<Question> Search(int userId, string keyword, int currentPage = 0, int take = 50)
        {
            int skip = Convert.ToInt32(currentPage * take);
            const string query = @"SELECT [Q].*,[QC].[QuestionCategoryName], ISNULL(COUNT(*) OVER(),0) AS TotalRecordCount 
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
            return _dbContext.SqlConnection.Query<Question>(query, new { UserId = userId, Keyword = keyword, Skip = skip, Take = take }).ToList();
        }

        public IEnumerable<Question> SearchByTestId(int testId, string keyword, int currentPage = 0, int take = 50, bool mappedQuestionOnly = false, bool withAnswerOption = false, bool withCorrectAnswerOption = false)
        {
            int skip = Convert.ToInt32(currentPage * take);
            const string query = @"SELECT DISTINCT [Q].*,[QC].QuestionCategoryName, [TWQ].TestId, ISNULL(COUNT(*) OVER(),0) AS TotalRecordCount 
                            FROM [dbo].[Question] [Q]
                            LEFT JOIN [dbo].[QuestionCategory] [QC] ON [Q].QuestionCategoryId = [QC].QuestionCategoryId
                            LEFT JOIN (SELECT TWQ.QuestionId,TWQ.TestId FROM  [dbo].[TestWiseQuestion] [TWQ] WHERE  [TWQ].[TestId] =@TestId 
                            )TWQ ON TWQ.QuestionId = Q.QuestionId
                            WHERE 1 = 1
                            AND (@TestId = 0 OR @MappedOnly = 0 OR [TWQ].TestId = (CASE WHEN @MappedOnly = 1 THEN @TestId ELSE [TWQ].TestId END))
                            AND (LOWER([Q].[QuestionText]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([Q].[Tags]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([Q].[Marks]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([Q].[DifficultyLevel]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([QC].[QuestionCategoryName]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([Q].[AnswerExplanation]) LIKE  '%' + LOWER(@Keyword) + '%')
                            ORDER BY [Q].[QuestionId] DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            var questionList= _dbContext.SqlConnection.Query<Question>(query, new { TestId = testId, Keyword = keyword, Skip = skip, Take = take, MappedOnly=mappedQuestionOnly?1 : 0 }).ToList();

            if (withAnswerOption)
            {
                if (questionList.Any())
                {
                    for (int i = 0; i < questionList.Count(); i++)
                    {
                        var answerOption = _iQuestionAnswerOptionRepository.GetByQuestionId(questionList.ElementAt(i).QuestionId, withCorrectAnswerOption);
                        questionList.ElementAt(i).QuestionAnswerOptionList = answerOption;
                    }
                }
            }

            return questionList;

        }

        public Question GetById(int questionId)
        {

            const string query = @"SELECT [Q].*,[QC].[QuestionCategoryName] FROM [dbo].[Question] [Q] INNER JOIN [dbo].[QuestionCategory] [QC] ON [QC].[QuestionCategoryId] = [Q].[QuestionCategoryId] WHERE [Q].[QuestionId] = @QuestionId; SELECT [QAO].* FROM [dbo].[QuestionAnswerOption] [QAO] WHERE [QAO].[QuestionId] = @QuestionId";

            var queryMultiple = _dbContext.SqlConnection.QueryMultiple(query, new { QuestionId = questionId });

            Question question = queryMultiple.Read<Question>().FirstOrDefault();

            if (question != null)
            {
                List<QuestionAnswerOption> questionAnswerOptionList = queryMultiple.Read<QuestionAnswerOption>().ToList();

                question.QuestionAnswerOptionList = questionAnswerOptionList;
                
                return question;
            }

            return null;
        }
    }

    public interface IQuestionRepository : IBaseRepository<Question>
    {
        IEnumerable<Question> Search(int userId, string keyword, int currentPage = 0, int take = 50);
        IEnumerable<Question> SearchByTestId(int testId, string keyword, int currentPage = 0, int take = 50, bool mappedQuestionOnly = false, bool withAnswerOption = false, bool withCorrectAnswerOption = false);
        Question GetById(int questionId);
    }
}
