using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using XSDValidationTest2.Models;
using XSDValidationTest2.Validator;

namespace XSDValidationTest2.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index() => View(new ConfigModel());

        [HttpPost]
        public ActionResult UploadFile(IEnumerable<HttpPostedFileBase> files)
        {
            try
            {
                string fileName = string.Empty;
                if (files != null)
                {
                    foreach (HttpPostedFileBase file in files)
                    {
                        fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        string physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                        file.SaveAs(physicalPath);
                    }
                }

                return Json(new { isSuccess = true, fileName = fileName }, "text/plain");
            }
            catch (Exception)
            {
                return Json(new { isSuccess = false }, "text/plain");
            }
        }

        [HttpPost]
        public ActionResult RemoveFile(string fileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    string physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                    if (System.IO.File.Exists(physicalPath))
                    {
                        System.IO.File.Delete(physicalPath);
                    }

                }
                return Json(new { isSuccess = true, fileName = fileName }, "text/plain");
            }
            catch (Exception)
            {
                return Json(new { isSuccess = false }, "text/plain");
            }
        }

        [HttpPost]
        public ActionResult ValidateXmlByXsd(string fileName)
        {
            string fileFullName = Path.Combine(Server.MapPath("~/App_Data"), fileName);

            ValidationResult result = new MessageValidator().ValidateFile(fileFullName);

            return Json(
                new
                {
                    isSuccess = result.IsValid,
                    message = result.Message
                },
                "text/plain");
        }
    }
}