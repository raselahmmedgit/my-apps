using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPEDU.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error()
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

        //[AcceptVerbs(HttpVerbs.Get)]
        //public ActionResult ShowContactPhoto(int? id, int? contactId, string PhotoFileName = null)
        //{
        //    if (contactId.HasValue && contactId.Value > 0)
        //    {
        //        id = contactId;
        //    }
        //    string fileBasePath = Constants.Paths.DownloadFilePath + "Contact/" + id.ToString() + "/";
        //    string filePath = string.Empty;
        //    if (PhotoFileName.IsNotNullOrEmpty())
        //    {
        //        string physicalPath = MiscUtility.CreateDirectory(fileBasePath) + PhotoFileName;
        //        if (System.IO.File.Exists(physicalPath))
        //        {
        //            filePath = fileBasePath + PhotoFileName;
        //        }
        //    }
        //    if (filePath.IsNullOrEmpty() && id.HasValue)
        //    {
        //        if (contactRepository == null)
        //        {
        //            contactRepository = DependencyResolver.Current.GetService(typeof(IContactRepository)) as IContactRepository;
        //        }
        //        Contact contact = contactRepository.Find(id.Value);
        //        if (contact != null && contact.PhotoFileName.IsNotNullOrEmpty())
        //        {
        //            PhotoFileName = contact.PhotoFileName;
        //            string physicalPath = MiscUtility.CreateDirectory(fileBasePath) + PhotoFileName;
        //            if (!System.IO.File.Exists(physicalPath))
        //            {
        //                Utility.WriteFile(physicalPath, contact.Photo);
        //            }
        //            filePath = fileBasePath + PhotoFileName;
        //        }
        //    }
        //    if (filePath.IsNullOrEmpty() || !filePath.IsImage())
        //    {
        //        filePath = Url.Content("~/assets/global/img/no-photo.png");
        //        PhotoFileName = "no-photo.png";
        //    }
        //    ImageResult result = new ImageResult(filePath, Utility.GetMIMEType(PhotoFileName));
        //    return result;
        //}
    }
}