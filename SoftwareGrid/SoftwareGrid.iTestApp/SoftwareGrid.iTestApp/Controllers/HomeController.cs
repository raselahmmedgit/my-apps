using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using SoftwareGrid.Model.iTestApp.QuestionManagement;
using SoftwareGrid.Model.iTestApp.SecurityManagement;
using SoftwareGrid.Model.iTestApp.TestManagement;
using SoftwareGrid.Model.iTestApp.Utility;
using SoftwareGrid.Service.iTestApp.Helper;
using SoftwareGrid.Service.iTestApp.QuestionManagement;
using SoftwareGrid.Service.iTestApp.TestManagement;
using SoftwareGrid.Service.iTestApp.UserManagement;

namespace SoftwareGrid.iTestApp.Controllers
{
    public class HomeController : BaseController
    {
        #region Private Variable
        private readonly IUserService _iUserService;
        private readonly ITestCategoryService _iTestCategoryService;
        private readonly ITestService _iTestService;
        private readonly IUserLoginInformationService _iUserLoginInformationService;
        private readonly IQuestionService _iQuestionService;
        private readonly ITestTakenService _iTestTakenService;
        private readonly IFavoriteTestService _iFavoriteTestService;
        #endregion

        #region Constructor

        public HomeController(
              IUserService iUserService
            , ITestCategoryService iTestCategoryService
            , ITestService iTestService
            , IQuestionService iQuestionService
            , ITestTakenService iTestTakenService
            , IUserLoginInformationService iUserLoginInformationService
            , IFavoriteTestService iFavoriteTestService
            )
        {
            _iUserService = iUserService;
            _iTestCategoryService = iTestCategoryService;
            _iTestService = iTestService;
            _iQuestionService = iQuestionService;
            _iTestTakenService = iTestTakenService;
            _iUserLoginInformationService = iUserLoginInformationService;
            _iFavoriteTestService = iFavoriteTestService;
        }

        #endregion

        #region General Action

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IndexAjax()
        {
            var testList = _iTestService.GetTestIconForHomePage();
            foreach (var test in testList)
            {
                test.TestIconPath = GetImagePath(test.GlobalId, test.TestIconName);
            }
            return Json(testList, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Login
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(User user)
        {
            var info = _iUserService.GetUser(user.Email, user.Password);
            if (info == null)
            {
                ViewBag.Message = "Invalid username/password";
                return View();
            }
            await SignInAsync(info, true);
            WebHelper.CurrentSession.Content.LoggedInUser = info;
            var newCookieHelper = new CookieHelper();
            newCookieHelper.SetLoginCookie(user.Email, user.UserId.ToString(), true);
            if (user.RememberMe)
            {
                newCookieHelper.RememberMe(user.Email, user.Password);
            }
            else
            {
                newCookieHelper.ForgetMe();
            }

            string returnUrl = string.Empty;
            if (Request.UrlReferrer != null)
            {
                var returnNameValueCollection = HttpUtility.ParseQueryString(Request.UrlReferrer.Query);
                if (returnNameValueCollection["ReturnUrl"] != null)
                {
                    returnUrl = HttpUtility.UrlDecode(returnNameValueCollection["ReturnUrl"]);
                }
            }
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect("/Home/Index");
            }


        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private async Task SignInAsync(User user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Sid, "itestapp_"+Convert.ToString(user.UserId)+"_"+user.Email));
            claims.Add(new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, AntiForgeryConfig.UniqueClaimTypeIdentifier));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);

            _iUserLoginInformationService.InsertOrUpdateWithoutIdentity(new UserLoginInformation
            {
                UserId = user.UserId,
                LoginDateTime = DateTime.UtcNow,
                IpAddress = Request.UserHostAddress
            });
        }


        #endregion

        #region Logout

        public ActionResult Logout()
        {
            var session = WebHelper.CurrentSession.Content.LoggedInUser;
            if (session != null)
            {
                _iUserLoginInformationService.UpdateUserLogoutTime(session.UserId);
            }

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            FormsAuthentication.SignOut();
            WebHelper.CurrentSession.Clear();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            var cookiehelper = new CookieHelper();
            cookiehelper.RemoveAll();
            return RedirectToAction("Login", "Home", new { Area = string.Empty });

        }

        #endregion

        #region Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(User user)
        {
            var message = _iUserService.Register(user, null);
            await SignInAsync(user, true);
            WebHelper.CurrentSession.Content.LoggedInUser = user;
            var newCookieHelper = new CookieHelper();
            newCookieHelper.SetLoginCookie(user.Email, user.UserId.ToString(), true);
            if (user.RememberMe)
            {
                newCookieHelper.RememberMe(user.Email, user.Password);
            }
            else
            {
                newCookieHelper.ForgetMe();
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Unauthorized Access

        public ActionResult Unauthorized()
        {
            return View();
        }

        #endregion

        #endregion

        #region Test library
        public ActionResult TestCategoryAjax()
        {
            var testCategoryList = _iTestCategoryService.GetAll();
            return Json(testCategoryList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TestLibrary()
        {
            return View();
        }

        public ActionResult TestLibraryAjax(int testCategoryId, int iDisplayStart = 0, int iDisplayLength = 15)
        {
            int userId = 0;
            if (User.Identity.IsAuthenticated)
            {
                userId = WebHelper.CurrentSession.Content.LoggedInUser.UserId;
            }

            var testList = _iTestService.GetTestWithUserByTestCategoryId(userId,testCategoryId, iDisplayStart, iDisplayLength);
            foreach (var test in testList)
            {
                test.TestIconPath = GetImagePath(test.GlobalId, test.TestIconName);
            }
            return Json(testList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TestDetails(int id)
        {
            var test = _iTestService.GetById(id);
            test.TestIconPath = GetImagePath(test.GlobalId, test.TestIconName);
            return View(test);
        }


        #endregion

        #region Test Taken

        [UserAuthorize]
        public ActionResult TestTaken(int testId)
        {
            var test = _iTestService.Get(new Test { TestId = testId });
            return View(test);
        }

        public ActionResult SearchByTestId(int testId, int iDisplayStart = 0, int iDisplayLength = 1)
        {
            var data = _iQuestionService.SearchByTestId(testId, "", iDisplayStart, iDisplayLength, true, true, false);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize]
        [HttpPost]
        public ActionResult StartTest(TestTaken testTaken)
        {
            testTaken.UserId = WebHelper.CurrentSession.Content.LoggedInUser.UserId;
            testTaken.AccessCode = testTaken.AccessCode;
            testTaken.StartTime = DateTime.UtcNow;
            testTaken.IpAddress = Request.UserHostAddress;
            var data = _iTestTakenService.InsertOrUpdateWithoutIdentity(testTaken);
            return Json(testTaken, JsonRequestBehavior.AllowGet);
        }


        [UserAuthorize]
        [HttpPost]
        public ActionResult FinishTest(TestTaken testTaken, string answerOption)
        {
            var details = JsonConvert.DeserializeObject<List<TestTakenDetail>>(answerOption);
            testTaken.TestTakenDetails = details;
            var message = _iTestTakenService.FinishTest(testTaken);
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize]
        public ActionResult TestResult(int takenId)
        {
            var testResult = _iTestTakenService.GetById(takenId, true, true);
            return View(testResult);
        }

        #endregion

        #region My Taken Test

        [UserAuthorize]
        public ActionResult MyTakenTest()
        {
            return View();
        }


        [UserAuthorize]
        public ActionResult MyTakenTestAjax(int iDisplayStart = 0, int iDisplayLength = 15)
        {
            var userId = WebHelper.CurrentSession.Content.LoggedInUser.UserId;
            var testList = _iTestService.GetTestTakenByUserId(userId, iDisplayStart, iDisplayLength);
            foreach (var test in testList)
            {
                test.TestIconPath = GetImagePath(test.GlobalId, test.TestIconName);
            }
            return Json(testList, JsonRequestBehavior.AllowGet);
        }



        #endregion

        #region Favorite Test

        
        [HttpPost]
        public ActionResult AddFavoriteTest(FavoriteTest favoriteTest)
        {
            if (User.Identity.IsAuthenticated)
            {
                favoriteTest.UserId = WebHelper.CurrentSession.Content.LoggedInUser.UserId;
                var message = _iFavoriteTestService.AddToFavorite(favoriteTest);
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var message = SetMessage.SetLoginRequiredMessage();
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        [UserAuthorize]
        [HttpPost]
        public ActionResult RemoveFavoriteTest(int sl)
        {
            var message = _iFavoriteTestService.Delete(new FavoriteTest { SL = sl });
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [UserAuthorize]
        public ActionResult MyFavoriteTest()
        {
            return View();
        }


        [UserAuthorize]
        public ActionResult MyFavoriteTestAjax(int iDisplayStart = 0, int iDisplayLength = 15)
        {
            var userId = WebHelper.CurrentSession.Content.LoggedInUser.UserId;
            var testList = _iTestService.GetFavoriteTestByUserId(userId, iDisplayStart, iDisplayLength);
            foreach (var test in testList)
            {
                test.TestIconPath = GetImagePath(test.GlobalId, test.TestIconName);
            }
            return Json(testList, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region External Access
        /// <summary>
        /// Raasforce user will be access this method.  
        /// </summary>
        /// <param name="param1">First Name</param>
        /// <param name="param2">Last Name</param>
        /// <param name="param3">Email Address</param>
        /// <param name="param4">Password</param>
        /// <param name="param5">User Id</param>
        /// <param name="param6">Request Type: 1=TestDetails, 2=Test Result</param>
        /// <param name="param7">Requested Id</param>
        /// <returns></returns>
        public async Task<ActionResult> ExternalAccess(string param1, string param2, string param3, string param4, string param5, string param6, string param7)
        {
            param3 = param3.Replace(" ", "+");
            param4 = param4.Replace(" ", "+");
            var emailAddress = CryptographyHelper.Decrypt(param3);
            var password = CryptographyHelper.Decrypt(param4);
            var user = new User();
            if (!User.Identity.IsAuthenticated)
            {
                user.FirstName = param1;
                user.LastName = param2;
                user.Email = emailAddress;
                user.Password = password;
                //if (!string.IsNullOrEmpty(param5))
                //{
                //    user.RaasForceUserId = Convert.ToInt32(param5);
                //}
                
                var message = _iUserService.Register(user, null);
                if (message.MessageType == MessageType.Success)
                {
                    await SignInAsync(user, true);
                    WebHelper.CurrentSession.Content.LoggedInUser = user;
                    var newCookieHelper = new CookieHelper();
                    newCookieHelper.SetLoginCookie(user.Email, user.UserId.ToString(), true);
                }
            }
            if (param6 == "1")
            {
                var url = "/Home/TestDetails?id=" + param7;
                return Redirect(url);
            }
            if (param6 == "2")
            {
                var url = "/Home/TestResult?takenId=" + param7;
                return Redirect(url);
            }
            return View();
        }

        #endregion
    }
}