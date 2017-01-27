using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using rabapp.Model.Quiz.ViewModels;
using rabapp.Repository.Common;
using rabapp.Repository.Quiz.QuestionManagement;
using rabapp.Repository.Quiz.SecurityManagement;
using rabapp.Repository.Quiz.TestManagement;
using rabapp.Service.Common;

namespace rabapp.Service.Quiz.TestManagement
{
    public class TestTakenService : BaseService<TestTakenViewModel>, ITestTakenService
    {
        private readonly ITestTakenRepository _iTestTakenRepository;
        private readonly ITestTakenDetailsRepository _iTestTakenDetailsRepository;
        private readonly ITestRepository _iTestRepository;
        private readonly IQuestionAnswerOptionRepository _iQuestionAnswerOptionRepository;
        private readonly IUserRepository _iUserRepository;
        private readonly AppDbContext _appDbContext;
        public TestTakenService(
              IBaseRepository<TestTakenViewModel> iBaseRepository
            , ITestTakenRepository iTestTakenRepository
            , ITestTakenDetailsRepository iTestTakenDetailsRepository
            , IQuestionAnswerOptionRepository iQuestionAnswerOptionRepository
            , ITestRepository iTestRepository
            , IUserRepository iUserRepository
            , AppDbContext appDbContext)
            : base(iBaseRepository, appDbContext)
        {
            _iTestTakenRepository = iTestTakenRepository;
            _iTestTakenDetailsRepository = iTestTakenDetailsRepository;
            _iTestRepository = iTestRepository;
            _iUserRepository = iUserRepository;
            _iQuestionAnswerOptionRepository = iQuestionAnswerOptionRepository;
            _appDbContext = appDbContext;
        }

        public Message FinishTest(TestTakenViewModel testTakenViewModel)
        {
            Message message;
            try
            {
                using (var scope = new TransactionScope())
                {
                    _appDbContext.SqlConnection.Open();
                    if (testTakenViewModel.TestTakenId == 0)
                    {
                        return SetMessage.SetErrorMessage("Taken Information not found. System unable to process your test result without taken id.");
                    }
                    var isExist = _iTestTakenRepository.Get(testTakenViewModel);
                    var affectedRow = 0;

                    isExist.EndTime = DateTime.UtcNow;

                    var testInformation = _iTestRepository.Get(new TestViewModel { TestId = testTakenViewModel.TestId });
                    var correctAnswer = 0;
                    foreach (var details in testTakenViewModel.TestTakenDetailsViewModelList)
                    {
                        details.TakenId = isExist.TestTakenId;
                        details.TestId = isExist.TestId;

                        var correctQuestionAnswerOptionId = String.Join(",", _iQuestionAnswerOptionRepository.GetByQuestionId(details.QuestionId, true).Where(c => c.IsCorrectAnswer).Select(a => a.QuestionAnswerOptionId));
                        var givenQuestionAnswerOptionId = String.Join(",", testTakenViewModel.TestTakenDetailsViewModelList.Where(c => c.QuestionId == details.QuestionId).Select(a => a.QuestionAnswerOptionId));

                        if (correctQuestionAnswerOptionId == givenQuestionAnswerOptionId)
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

                    var user = _iUserRepository.Get(new UserViewModel() { UserId = isExist.UserId });

                    if (user != null && user.UserId != null && user.UserId > 0)
                    {
                        
                    }

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
                _appDbContext.SqlConnection.Close();
            }
            return message;
        }

        public TestTakenViewModel GetById(int takenId, bool withDetails = false, bool withNavigationProperty = false)
        {
            try
            {
                _appDbContext.SqlConnection.Open();

                return _iTestTakenRepository.GetById(takenId, withDetails, withNavigationProperty);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                _appDbContext.SqlConnection.Close();
            }
        }
    }

    public interface ITestTakenService : IBaseService<TestTakenViewModel>
    {
        Message FinishTest(TestTakenViewModel testTakenViewModel);
        TestTakenViewModel GetById(int takenId, bool withDetails = false, bool withNavigationProperty = false);
    }
}
