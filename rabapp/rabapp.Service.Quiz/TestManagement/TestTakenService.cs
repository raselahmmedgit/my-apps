using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabapp.Service.Quiz.TestManagement
{
    public class TestTakenService : BaseService<TestTaken>, ITestTakenService
    {
        private readonly ITestTakenRepository _iTestTakenRepository;
        private readonly ITestTakenDetailsRepository _iTestTakenDetailsRepository;
        private readonly ITestRepository _iTestRepository;
        private readonly IQuestionAnswerOptionRepository _iQuestionAnswerOptionRepository;
        private readonly DbContext _dbContext;
        public TestTakenService(
              IBaseRepository<TestTaken> iBaseRepository
            , ITestTakenRepository iTestTakenRepository
            , ITestTakenDetailsRepository iTestTakenDetailsRepository
            , IQuestionAnswerOptionRepository iQuestionAnswerOptionRepository
            , ITestRepository iTestRepository
            , DbContext dbContext)
            : base(iBaseRepository, dbContext)
        {
            _iTestTakenRepository = iTestTakenRepository;
            _iTestTakenDetailsRepository = iTestTakenDetailsRepository;
            _iTestRepository = iTestRepository;
            _iQuestionAnswerOptionRepository = iQuestionAnswerOptionRepository;
            _dbContext = dbContext;
        }

        public Message FinishTest(TestTaken testTaken)
        {
            Message message;
            try
            {
                using (var scope = new TransactionScope())
                {
                    _dbContext.SqlConnection.Open();
                    if (testTaken.TakenId == 0)
                    {
                        return SetMessage.SetErrorMessage("Taken Information not found. System unable to process your test result without taken id.");
                    }
                    var isExist = _iTestTakenRepository.Get(testTaken);
                    var affectedRow = 0;

                    isExist.EndTime = DateTime.UtcNow;

                    var testInformation = _iTestRepository.Get(new Test { TestId = testTaken.TestId });
                    var correctAnswer = 0;
                    foreach (var details in testTaken.TestTakenDetails)
                    {
                        details.TakenId = isExist.TakenId;
                        details.TestId = isExist.TestId;

                        var correctAnswerOption = String.Join(",", _iQuestionAnswerOptionRepository.GetByQuestionId(details.QuestionId, true).Where(c => c.IsCorrectAnswer).Select(a => a.AnswerOptionId));
                        var givenAnswerOption = String.Join(",", testTaken.TestTakenDetails.Where(c => c.QuestionId == details.QuestionId).Select(a => a.AnswerOptionId));

                        if (correctAnswerOption == givenAnswerOption)
                        {
                            details.IsCorrectAnswer = true;
                            correctAnswer++;
                        }

                        _iTestTakenDetailsRepository.InsertWithoutIdentity(details);
                    }

                    var totalQuestion = testInformation.NoOfQuestion;
                    var score = Convert.ToDecimal((correctAnswer * 100.0 / totalQuestion));
                    isExist.Score = score;
                    affectedRow = _iTestTakenRepository.Update(isExist);

                    message = affectedRow > 0
                        ? SetMessage.SetSuccessMessage("Information has been saved successfully.")
                        : SetMessage.SetInformationMessage("No data has been saved.");

                    scope.Complete();
                }
            }
            catch (Exception exception)
            {
                return SetMessage.SetErrorMessage(exception.Message);
            }
            finally
            {
                _dbContext.SqlConnection.Close();
            }
            return message;
        }

        public TestTaken GetById(int takenId, bool withDetails = false, bool withNavigationProperty = false)
        {
            try
            {
                _dbContext.SqlConnection.Open();

                return _iTestTakenRepository.GetById(takenId, withDetails, withNavigationProperty);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                _dbContext.SqlConnection.Close();
            }
        }
    }

    public interface ITestTakenService : IBaseService<TestTaken>
    {
        Message FinishTest(TestTaken testTaken);
        TestTaken GetById(int takenId, bool withDetails = false, bool withNavigationProperty = false);
    }
}
