using SPEDU.Business.Application;
using SPEDU.Common.Extensions;
using SPEDU.Common.Helper;
using SPEDU.Common.Utility;
using SPEDU.Domain.Models.Application;
using SPEDU.Web.Helpers;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SPEDU
{
    //[Authorize]
    //[UserAuthorize]
    [ClearCache]
    public class BaseController : Controller
    {
        #region Global Variable Declaration

        #endregion

        #region Constructor

        public BaseController()
        {
        }

        #endregion

        #region Action

        //public User CurrentLoggedInUser
        //{
        //    get
        //    {
        //        User user = WebHelper.CurrentSession.Content.LoggedInUser;
        //        if (user == null)
        //        {
        //            int userId = 0;
        //            string emailAddress = string.Empty;
        //            if (HttpContext.User != null && HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
        //            {
        //                var identity = (ClaimsIdentity)User.Identity;
        //                IEnumerable<Claim> claims = identity.Claims;
        //                foreach (Claim claim in claims)
        //                {
        //                    if (claim.Type.Contains("identity/claims/sid"))
        //                    {
        //                        userId = claim.Value.ToInteger(true);
        //                        break;
        //                    }
        //                    else if (claim.Type.Contains("identity/claims/emailaddress"))
        //                    {
        //                        emailAddress = claim.Value;
        //                    }
        //                }
        //            }
        //            if (userId > 0 || emailAddress.IsNotNullOrEmpty())
        //            {
        //                if (userRepository == null)
        //                {
        //                    userRepository = DependencyResolver.Current.GetService(typeof(IUserRepository)) as IUserRepository;
        //                }
        //                if (userId > 0)
        //                {
        //                    user = userRepository.Find(userId);
        //                }
        //                else if (emailAddress.IsNotNullOrEmpty())
        //                {
        //                    user = userRepository.Find(emailAddress);
        //                }
        //                WebHelper.CurrentSession.Content.LoggedInUser = user;
        //            }
        //            if (user == null)
        //            {
        //                user = new User();
        //            }
        //        }
        //        return user;
        //    }
        //}

        private static readonly string[] _validFileExtension = new string[] { ".png", ".jpg", ".jpeg", ".gif", ".txt", ".doc", ".docx", ".pdf", ".xls", ".xlsx", ".csv", ".wmv", ".ppt", ".pptx", ".rar", ".zip" };

        [HttpPost]
        public ActionResult UploadFile()
        {
            try
            {
                HttpFileCollectionBase files = Request.Files;
                if (files != null && files.Count > 0)
                {
                    // Some browsers send file names with full path. This needs to be stripped.

                    var extension = Path.GetExtension(files[0].FileName);
                    if (_validFileExtension.Contains(extension))
                    {
                        var fileName = Path.GetFileName(files[0].FileName);
                        var physicalPath = Path.Combine(Server.MapPath(SPEDU.Common.Utility.AppConstant.Paths.TemporaryFileUploadPath), fileName);
                        Session.Add(files.AllKeys[0] + "FileName", fileName);

                        // The files are not actually saved in this demo
                        files[0].SaveAs(physicalPath);
                        return Content("");
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return Json("Unsuccessful", JsonRequestBehavior.DenyGet);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.Manage(ex, true);
            }
            return null;
            // Return an empty string to signify success
        }

        public ActionResult RemoveFile(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath(SPEDU.Common.Utility.AppConstant.Paths.TemporaryFileUploadPath), fileName);

                // TODO: Verify user permissions
                if (System.IO.File.Exists(physicalPath))
                {
                    // The files are not actually removed in this demo
                    System.IO.File.Delete(physicalPath);
                    Session.Remove(fileName + "FileName");
                }
            }
            // Return an empty string to signify success
            return Content("");
        }

        #region Show Image

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ShowImage(int id, string documentName)
        {
            const string fileBasePath = SPEDU.Common.Utility.AppConstant.ImagePath.ImageFolderPath;
            string filePath = string.Empty;
            if (documentName.IsNotNullOrEmpty() && id > 0)
            {
                string physicalPath = System.Web.HttpContext.Current.Server.MapPath(fileBasePath) + documentName;
                if (System.IO.File.Exists(physicalPath))
                {
                    filePath = Url.Content(fileBasePath + documentName);
                }
                else
                {
                    var iDocumentInfoRepository = DependencyResolver.Current.GetService(typeof(IDocumentInfoRepository)) as IDocumentInfoRepository;
                    var documentInfo = iDocumentInfoRepository.GetById(id);
                    if (documentInfo != null && documentInfo.DocumentName.IsNotNullOrEmpty())
                    {
                        documentName = documentInfo.DocumentName;
                        if (!System.IO.File.Exists(physicalPath))
                        {
                            CreateDirectory(fileBasePath);
                            if (!System.IO.File.Exists(physicalPath))
                            {
                                ImageHelper.WriteFile(physicalPath, documentInfo.DocumentByte);
                            }

                        }
                        filePath = Url.Content(fileBasePath + documentName);
                    }

                }
            }

            if (filePath.IsNullOrEmpty() || !filePath.IsImage())
            {
                filePath = Url.Content("~/Theme/img/no-image.png");
                documentName = "no-photo.jpg";
            }
            var result = new ImageResult(filePath, ImageHelper.GetMIMEType(documentName));
            return result;
        }

        public string GetImagePath(int id, string documentName)
        {
            string filePath = string.Empty;
            const string fileBasePath = SPEDU.Common.Utility.AppConstant.ImagePath.ImageFolderPath;
            if (documentName.IsNotNullOrEmpty() && id > 0)
            {
                string physicalPath = System.Web.HttpContext.Current.Server.MapPath(fileBasePath) + documentName;
                if (System.IO.File.Exists(physicalPath))
                {
                    filePath = Url.Content(fileBasePath + documentName);
                }
                else
                {
                    //var iDocumentInfoRepository = DependencyResolver.Current.GetService(typeof(IDocumentInfoRepository)) as IDocumentInfoRepository;
                    //var documentInfo = iDocumentInfoRepository.GetById(id);
                    //if (documentInfo != null && documentInfo.DocumentName.IsNotNullOrEmpty())
                    //{
                    //    documentName = documentInfo.DocumentName;
                    //    if (!System.IO.File.Exists(physicalPath))
                    //    {
                    //        CreateDirectory(fileBasePath);
                    //        if (!System.IO.File.Exists(physicalPath))
                    //        {
                    //            ImageHelper.WriteFile(physicalPath, documentInfo.DocumentByte);
                    //        }

                    //    }
                    //    filePath = Url.Content(fileBasePath + documentName);
                    //}

                }

            }
            if (filePath.IsNullOrEmpty() || !filePath.IsImage())
            {
                filePath = Url.Content("~/Theme/img/no-image.png");
            }
            string result = filePath;
            return result;
        }

        private string CreateDirectory(string directoryName)
        {
            try
            {
                directoryName = System.Web.HttpContext.Current.Server.MapPath(directoryName);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                return directoryName;
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region Get Current Action, Controller, Area
            string currentActionName = string.Empty;
            string currentControllerName = string.Empty;
            string currentAreaName = string.Empty;

            object objCurrentControllerName;
            this.RouteData.Values.TryGetValue("controller", out objCurrentControllerName);

            object objCurrentActionName;
            this.RouteData.Values.TryGetValue("action", out objCurrentActionName);

            if (this.RouteData.DataTokens.ContainsKey("area"))
            {
                currentAreaName = this.RouteData.DataTokens["area"].ToString();
            }
            if (objCurrentActionName != null)
            {
                currentActionName = objCurrentActionName.ToString();
            }
            if (objCurrentControllerName != null)
            {
                currentControllerName = objCurrentControllerName.ToString();
            }
            #endregion

            if (filterContext != null)
            {
                HttpSessionStateBase httpSessionStateBase = filterContext.HttpContext.Session;
                if (httpSessionStateBase != null)
                {
                    //var userSession = httpSessionStateBase["User"];
                    //if (((userSession == null) && (!httpSessionStateBase.IsNewSession)) || (httpSessionStateBase.IsNewSession))
                    //{
                    //    httpSessionStateBase.RemoveAll();
                    //    httpSessionStateBase.Clear();
                    //    httpSessionStateBase.Abandon();
                    //    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    //    {
                    //        filterContext.HttpContext.Response.StatusCode = 403;
                    //        filterContext.Result = new JsonResult
                    //        {
                    //            Data = new
                    //            {
                    //                // put whatever data you want which will be sent
                    //                // to the client
                    //                message = "Sorry, you are not logged user."
                    //            },
                    //            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    //        };
                    //    }
                    //    else
                    //    {
                    //        filterContext.Result = new RedirectResult("~/Home/Index");
                    //    }
                    //}

                    if (!CheckIfUserIsAuthenticated(filterContext))
                    {
                        if (filterContext.HttpContext.Request.IsAjaxRequest())
                        {
                            filterContext.HttpContext.Response.StatusCode = 403;
                            filterContext.Result = new JsonResult
                            {
                                Data = new
                                {
                                    // put whatever data you want which will be sent
                                    // to the client
                                    message = MessageResourceHelper.UnAuthenticated
                                },
                                JsonRequestBehavior = JsonRequestBehavior.AllowGet
                            };
                        }
                        else
                        {
                            if (filterContext.HttpContext.Request.Url != null)
                            {
                                string redirectUrl = string.Format("?returnUrl={0}", filterContext.HttpContext.Request.Url.PathAndQuery);

                                filterContext.HttpContext.Response.Redirect(FormsAuthentication.LoginUrl + redirectUrl, true);
                            }
                        }
                    }
                    else
                    {

                        if (!CheckIfUserAccessRight(currentActionName, currentControllerName, currentAreaName))
                        {
                            if (filterContext.HttpContext.Request.IsAjaxRequest())
                            {
                                filterContext.HttpContext.Response.StatusCode = 403;
                                filterContext.Result = new JsonResult
                                {
                                    Data = new
                                    {
                                        // put whatever data you want which will be sent
                                        // to the client
                                        message = MessageResourceHelper.UnAuthenticated
                                    },
                                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                                };
                            }
                            else
                            {
                                if (filterContext.HttpContext.Request.Url != null)
                                {
                                    string redirectUrl = string.Format("?returnUrl={0}", filterContext.HttpContext.Request.Url.PathAndQuery);

                                    filterContext.HttpContext.Response.Redirect(FormsAuthentication.LoginUrl + redirectUrl, true);
                                }
                            }
                        }
                        else
                        {
                            base.OnActionExecuting(filterContext);
                        }
                    }
                }
            }
        }

        #region Check User Authenticated
        private bool CheckIfUserIsAuthenticated(ActionExecutingContext filterContext)
        {
            // If Result is null, we’re OK: the user is authenticated and authorized. 
            if (filterContext.Result == null)
            {
                return true;
            }

            // If here, you’re getting an HTTP 401 status code. In particular,
            // filterContext.Result is of HttpUnauthorizedResult type. Check Ajax here. 
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckIfUserAccessRight(string actionName, string controllerName, string areaName)
        {
            return true;
        }
        #endregion

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
            {
                cultureName = cultureCookie.Value;
            }
            else
            {
                cultureCookie = Request.Cookies["app.culture"];
                if (cultureCookie != null)
                {
                    cultureName = cultureCookie.Value;
                }
                else
                {
                    cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0
                        ? Request.UserLanguages[0]
                        : null; // obtain it from HTTP header AcceptLanguages   
                }
            }

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }

        public ActionResult ChangeLanguage(string langCode)
        {
            langCode = langCode;
            if (langCode.Length == 2)
            {
                if (SiteConfigurationReader.ActiveLanguages.Contains(langCode))
                {
                    var cultureInfo = new CultureInfo(langCode);
                    //routedata.Values["language"] = language;
                    Thread.CurrentThread.CurrentCulture = cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = cultureInfo;
                }
                else
                {
                    var cultureInfo = new CultureInfo("en");
                    Thread.CurrentThread.CurrentCulture = cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = cultureInfo;
                }
            }
            HttpCookie appCultureCookie = new HttpCookie("app.culture");
            appCultureCookie.Value = langCode;
            appCultureCookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(appCultureCookie);
            return Redirect(Request.UrlReferrer.ToString());
        }

        #endregion
    }
}