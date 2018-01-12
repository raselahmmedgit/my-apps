using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using SoftwareGrid.Model.iTestApp.DocumentManagement;
using SoftwareGrid.Model.iTestApp.Utility;
using SoftwareGrid.Service.iTestApp.DocumentManagement;
using SoftwareGrid.Service.iTestApp.Extensions;
using SoftwareGrid.Service.iTestApp.Helper;
using SoftwareGrid.Service.iTestApp.TestManagement;
using System.Collections;
using SoftwareGrid.Model.iTestApp.TestManagement;
using SoftwareGrid.Service.iTestApp.QuestionManagement;
using SoftwareGrid.Model.iTestApp.QuestionManagement;

namespace SoftwareGrid.iTestApp.Controllers
{
    [ClearCache]
    public class BaseController : Controller
    {
        #region Drop Down List

        public JsonResult LoadTestCategoryAjax()
        {
            var service = DependencyResolver.Current.GetService(typeof(ITestCategoryService)) as ITestCategoryService;
            IEnumerable testCategory = service.GetAll();
            var testCategoryList = (from TestCategory item in testCategory select new SelectListItem {Text = item.TestCategoryName, Value = item.TestCategoryId.ToString()}).ToList();
            return Json(testCategoryList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadQuestionCategoryAjax()
        {
            var service = DependencyResolver.Current.GetService(typeof(IQuestionCategoryService)) as IQuestionCategoryService;
            IEnumerable questionCategory = service.GetAll();
            var testCategoryList = (from QuestionCategory item in questionCategory select new SelectListItem { Text = item.QuestionCategoryName, Value = item.QuestionCategoryId.ToString() }).ToList();
            return Json(testCategoryList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadDifficultyLevelAjax()
        {
            var result = Enum.GetValues(typeof(DifficultyLevels))
                         .Cast<DifficultyLevels>()
                         .Select(e => new { Value = (int)e, Description = e.ToString() })
                         .ToList();
            var selectedResult = result.Select(item => new SelectListItem() { Text = item.Description, Value = item.Value.ToString() }).ToList();
            return Json(selectedResult, JsonRequestBehavior.AllowGet);

        }

        public JsonResult LoadUserAjax()
        {
            var result = Enum.GetValues(typeof(AppUsers)).Cast<AppUsers>().Select(e => new { Value = (int)e, Description = e.ToString() }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult LoadUserWithoutUserAjax()
        {
            var result = Enum.GetValues(typeof(AppUsers)).Cast<AppUsers>().Select(e => new { Value = (int)e, Description = e.ToString() }).ToList();
            var selectedResult = result.Select(item => new SelectListItem() { Text = item.Description, Value = item.Value.ToString() }).Where(ut => ut.Value != "2").ToList();
            return Json(selectedResult, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Show Image

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ShowImage(int globalId, string documentName, int type = 1)
        {
            const string fileBasePath = Constants.ImagePath.ImageFolderPath;
            string filePath = string.Empty;
            if (!string.IsNullOrEmpty(documentName) && globalId > 0)
            {
                string physicalPath = System.Web.HttpContext.Current.Server.MapPath(fileBasePath) + documentName;
                if (System.IO.File.Exists(physicalPath))
                {
                    filePath = Url.Content(fileBasePath + documentName);
                }
                else
                {
                    var service = DependencyResolver.Current.GetService(typeof(IDocumentInformationService)) as IDocumentInformationService;
                    var documentInformation = service.Get(new DocumentInformation() { GlobalId = globalId });
                    if (documentInformation != null && !string.IsNullOrEmpty(documentInformation.DocumentName))
                    {
                        documentName = documentInformation.DocumentName;
                        if (!System.IO.File.Exists(physicalPath))
                        {
                            CreateDirectory(fileBasePath);
                            if (!System.IO.File.Exists(physicalPath))
                            {
                                IOFileHelper.WriteFile(physicalPath, documentInformation.DocumentByte);
                            }

                        }
                        filePath = Url.Content(fileBasePath + documentName);
                    }

                }
            }

            if (string.IsNullOrEmpty(filePath) || !filePath.IsImage())
            {
                filePath = Url.Content("~/assets/images/no-image.png");
                documentName = "no-photo.jpg";
            }
            if (string.IsNullOrEmpty(filePath) && type == 2)
            {
                filePath = Url.Content("~/assets/images/no-profile-image.png");
                documentName = "no-profile-image.png";
            }
            var result = new ImageResult(filePath, IOFileHelper.GetMIMEType(documentName));
            return result;
        }
        
        public string GetImagePath(int globalId, string documentName)
        {
            string filePath = string.Empty;
            const string fileBasePath = Constants.ImagePath.ImageFolderPath;
            if (!string.IsNullOrEmpty(documentName) && globalId > 0)
            {
                string physicalPath = System.Web.HttpContext.Current.Server.MapPath(fileBasePath) + documentName;
                if (System.IO.File.Exists(physicalPath))
                {
                    filePath = Url.Content(fileBasePath + documentName);
                }
                else
                {
                    var service = DependencyResolver.Current.GetService(typeof(IDocumentInformationService)) as IDocumentInformationService;
                    var documentInformation = service.Get(new DocumentInformation() { GlobalId = globalId });
                    if (documentInformation != null && !string.IsNullOrEmpty(documentInformation.DocumentName))
                    {
                        documentName = documentInformation.DocumentName;
                        if (!System.IO.File.Exists(physicalPath))
                        {
                            CreateDirectory(fileBasePath);
                            if (!System.IO.File.Exists(physicalPath))
                            {
                                IOFileHelper.WriteFile(physicalPath, documentInformation.DocumentByte);
                            }
                            
                        }
                        filePath = Url.Content(fileBasePath + documentName);
                    }

                }

            }
            if (string.IsNullOrEmpty(filePath) || !filePath.IsImage())
            {
                filePath = Url.Content("~/assets/images/no-image.png");
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
    }
}