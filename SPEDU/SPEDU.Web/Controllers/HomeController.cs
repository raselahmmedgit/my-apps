using System.Web.Mvc;

namespace SPEDU.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Global Variable Declaration


        #endregion

        #region Constructor

        public HomeController()
        {
        }

        #endregion

        #region Action

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }

        public ActionResult Unauthorized()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult UploadFile()
        //{
        //    try
        //    {
        //        HttpFileCollectionBase files = Request.Files;
        //        if (files != null && files.Count > 0)
        //        {
        //            // Some browsers send file names with full path. This needs to be stripped.

        //            var extension = Path.GetExtension(files[0].FileName);
        //            if (_validTaskFileExtension.Contains(extension))
        //            {
        //                var fileName = Path.GetFileName(files[0].FileName);
        //                var physicalPath = Path.Combine(Server.MapPath(Constants.Paths.TemporaryFileUploadPath), fileName);
        //                Session.Add(files.AllKeys[0] + "FileName", fileName);

        //                // The files are not actually saved in this demo
        //                files[0].SaveAs(physicalPath);
        //                return Content("");
        //            }
        //            else
        //            {
        //                Response.StatusCode = 400;
        //                return Json("Unsuccessful", JsonRequestBehavior.DenyGet);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Manage(ex);
        //    }
        //    return null;
        //    // Return an empty string to signify success
        //}


        //public ActionResult RemoveFile(string[] fileNames)
        //{
        //    // The parameter of the Remove action must be called "fileNames"
        //    foreach (var fullName in fileNames)
        //    {
        //        var fileName = Path.GetFileName(fullName);
        //        var physicalPath = Path.Combine(Server.MapPath(Constants.Paths.TemporaryFileUploadPath), fileName);

        //        // TODO: Verify user permissions
        //        if (System.IO.File.Exists(physicalPath))
        //        {
        //            // The files are not actually removed in this demo
        //            System.IO.File.Delete(physicalPath);
        //            Session.Remove(fileName + "FileName");
        //        }
        //    }
        //    // Return an empty string to signify success
        //    return Content("");
        //}

        #endregion

        #region Method

        #endregion
    }
}