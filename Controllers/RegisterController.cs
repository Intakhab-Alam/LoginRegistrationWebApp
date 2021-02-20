using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginRegistrationWebApp.ViewModel;
using LoginRegistrationWebApp.Repositories;

namespace LoginRegistrationWebApp.Controllers
{
    public class RegisterController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveData(SiteUserViewModel objSiteUserViewModel)
        {
            SiteUserRepository objSiteUserRepository = new SiteUserRepository();
            objSiteUserRepository.AddUser(objSiteUserViewModel);

            return Json("Registration has been Successfully Completed", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Confirm(int regId)
        {
            ViewBag.regID = regId;
            return View();
        }
        [HttpPost]
        public JsonResult RegisterConfirm(int regId)
        {
            SiteUserRepository objSiteUserRepository = new SiteUserRepository();
            objSiteUserRepository.ConfirmUser(regId);

            var msg = "Your Email is Verified";

            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}