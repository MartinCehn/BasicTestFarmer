using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicTestFarmer.Models;


namespace BasicTestFarmer.Controllers
{
    public class LoginController : Controller
    {
        FarmerEntities db = new FarmerEntities();
        // GET: Login
        //會員介面
        public ActionResult Index()
        {
            var infoList = db.Member.OrderBy(m => m.UserID).ToList();
            return View(infoList);
        }

        //會員登入畫面
        public ActionResult login()
        {
            return View();
        }

        //會員登入畫面_POST
        [HttpPost]
        public ActionResult login(Member _form)
        {
            var islogin = db.Member.Any(p => p.UserAccount == _form.UserAccount && p.UsePass == _form.UsePass);
            if (islogin)
            {
                Response.Cookies["Login"].Value = "y";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}