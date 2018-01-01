using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SoftwareGrid.Model.iTestApp.QuestionManagement;
using SoftwareGrid.Service.iTestApp.DocumentManagement;
using SoftwareGrid.Service.iTestApp.Helper;
using SoftwareGrid.Service.iTestApp.QuestionManagement;
using SoftwareGrid.Model.iTestApp.TestManagement;
using SoftwareGrid.Service.iTestApp.TestManagement;
using System.Collections.Generic;
using SoftwareGrid.Model.iTestApp.SecurityManagement;
using SoftwareGrid.Service.iTestApp.UserManagement;
using SoftwareGrid.Service.iTestApp.Utility;

namespace SoftwareGrid.iTestApp.Controllers
{
    [UserAuthorize]
    [CheckPermission]
    public class AdminController : BaseController
    {
        #region Private Variable

        private readonly IQuestionCategoryService _iQuestionCategoryService;
        private readonly IQuestionService _iQuestionService;
        private readonly ITestCategoryService _iTestCategoryService;
        private readonly ITestService _iTestService;
        private readonly IUserService _iUserService;
        private readonly IUtilityService _iUtilityService;
        private readonly ITestTakenService _iTestTakenService;

        #endregion

        #region Constructor

        public AdminController(IQuestionCategoryService iQuestionCategoryService
            , IQuestionService iQuestionService
            , ITestCategoryService iTestCategoryService
            , ITestService iTestService
            , IDocumentInformationService iDocumentInformationService
            , IUserService iUserService
            , IUtilityService iUtilityService
            ,ITestTakenService iTestTakenService
            )
        {
            _iQuestionCategoryService = iQuestionCategoryService;
            _iTestCategoryService = iTestCategoryService;
            _iQuestionService = iQuestionService;
            _iTestService = iTestService;
            _iUserService = iUserService;
            _iUtilityService = iUtilityService;
            _iTestTakenService = iTestTakenService;
        }

        #endregion

        #region Action

        #region Dashboard
        public ActionResult Dashboard()
        {
            var session = WebHelper.CurrentSession.Content.LoggedInUser;
            var summary = _iUtilityService.GetDashboardSummaryCount(session.UserId);
            return View(summary);
        }
        #endregion

        #region Question Category

        public ActionResult QuestionCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateQuestionCategory(QuestionCategory questionCategory)
        {
            var message = _iQuestionCategoryService.InsertOrUpdateWithoutIdentity(questionCategory);
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetQuestionCategory(int questionCategoryId)
        {
            var message = _iQuestionCategoryService.Get(new QuestionCategory { QuestionCategoryId = questionCategoryId });
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAllQuestionCategory()
        {
            var message = _iQuestionCategoryService.GetAll();
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteQuestionCategory(int questionCategoryId)
        {
            var message = _iQuestionCategoryService.Delete(new QuestionCategory { QuestionCategoryId = questionCategoryId });
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Test Management

        #region Test Category
        public ActionResult TestCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTestCategory(TestCategory testCategory)
        {
            var message = _iTestCategoryService.InsertOrUpdateWithoutIdentity(testCategory);
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetTestCategory(int testCategoryId)
        {
            var message = _iTestCategoryService.Get(new TestCategory { TestCategoryId = testCategoryId });
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAllTestCategory()
        {
            var message = _iTestCategoryService.GetAll();
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteTestCategory(int testCategoryId)
        {
            var message = _iTestCategoryService.Delete(new TestCategory { TestCategoryId = testCategoryId });
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Test
        public ActionResult Test()
        {
            return View();
        }

        public ActionResult EditTest(int? testId)
        {
            TempData["TestId"] = testId;
            return RedirectToAction("Test");
        }

        public ActionResult TestList()
        {
            return View();
        }

        public JsonResult GetAllTest(string keyword, int iDisplayStart = 0, int iDisplayLength = 50)
        {
            var message = _iTestService.Search(keyword, iDisplayStart, iDisplayLength);
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTest(int testId)
        {
            var message = _iTestService.GetById(testId);
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateTest(Test test, HttpPostedFileBase file, string questionIds)
        {
            var testWiseQuestion = JsonConvert.DeserializeObject<List<TestWiseQuestion>>(questionIds);
            test.TestWiseQuestions = testWiseQuestion;
            var message = _iTestService.InsertOrUpdateWithoutIdentity(test, file);
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangeTestStatus(int testId, bool status)
        {
            var message = _iTestService.ChangeTestStatus(testId, status);
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region Question

        public ActionResult Question()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateQuestion(Question question, string questionAnswerOptionList, HttpPostedFileBase fileUpload)
        {
            var answerOptionList = JsonConvert.DeserializeObject<List<QuestionAnswerOption>>(questionAnswerOptionList);
            question.QuestionAnswerOptionList = answerOptionList;

            var message = _iQuestionService.InsertOrUpdateWithoutIdentity(question, fileUpload);
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetQuestion(int questionId)
        {
            var message = _iQuestionService.GetById(questionId);
            message.QuestionImagePath = GetImagePath(message.GlobalId, message.QuestionImageName);
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAllQuestion(string keyword, int iDisplayStart = 0, int iDisplayLength = 50)
        {
            var message = _iQuestionService.Search(keyword, iDisplayStart, iDisplayLength);
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAllQuestionByTestId(int testId, string keyword, int iDisplayStart = 0, int iDisplayLength = 50)
        {
            var message = _iQuestionService.SearchByTestId(testId, keyword, iDisplayStart, iDisplayLength);
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteQuestion(int questionId)
        {
            var message = _iQuestionService.Delete(new Question { QuestionId = questionId });
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Internal User Registration And User List
        public ActionResult InternalUserRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(User user, HttpPostedFileBase file)
        {
            var message = _iUserService.Register(user, file);
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UserList()
        {
            ViewBag.CurrentLoggedInUser = WebHelper.CurrentSession.Content.LoggedInUser.UserId;
            return View();
        }

        [HttpPost]
        public JsonResult GetAllUser(string keyword, int iDisplayStart = 0, int iDisplayLength = 50)
        {
            var message = _iUserService.DynamicSearch(keyword, iDisplayStart, iDisplayLength);
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region User Details

        public ActionResult UserProfile(int userId)
        {
            var summary = _iUtilityService.GetDashboardSummaryCount(userId);
            var userInfo = _iUserService.Get(new User {UserId = userId});
            ViewBag.Summary = summary;
            return View(userInfo);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateUser(User user, HttpPostedFileBase fileUpload,bool updateimageOnly)
        {
            var message = _iUserService.UpdateUserInformation(user, fileUpload, updateimageOnly);
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangePassword(User user)
        {
            var message = _iUserService.ChangePassword(user);
            return Json(message, JsonRequestBehavior.AllowGet);
        }


        [UserAuthorize]
        public ActionResult UserTakenTestAjax(int userId, int iDisplayStart = 0, int iDisplayLength = 15)
        {
            var testList = _iTestService.GetTestTakenByUserId(userId, iDisplayStart, iDisplayLength);
            foreach (var test in testList)
            {
                test.TestIconPath = GetImagePath(test.GlobalId, test.TestIconName);
            }
            return Json(testList, JsonRequestBehavior.AllowGet);
        }


        #endregion



        #endregion

    }
}