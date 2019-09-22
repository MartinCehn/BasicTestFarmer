using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicTestFarmer.Models;

namespace BasicTestFarmer.Areas.Backstage.Controllers
{
    public class MembersController : Controller
    {
        FarmerEntities db = new FarmerEntities();
        // GET: Backstage/Members
        //會員介面
        public ActionResult Index()
        {
            var infoList = db.Member.OrderBy(m => m.UserID).ToList();
            return View(infoList);
        }

        ////會員登入畫面
        //public ActionResult login() 
        //{
        //    return View();
        //}

        ////會員登入畫面
        //[HttpPost]
        //public ActionResult login(Member _form) 
        //{
        //    var islogin = db.Member.Any(p => p.UserAccount == _form.UserAccount && p.UsePass == _form.UsePass);
        //    if (islogin)
        //    {
        //        Response.Cookies["Login"].Value = "y";
        //        return RedirectToAction("Index");
        //    }

        //    return View();
        //}

        //修改會員資料
        public ActionResult Edit(int id) 
        {
            var editList = db.Member.Where(m => m.UserID == id).FirstOrDefault();
            return View(editList);
        }
        //修改資料使用POST
        //若遇到欄位Null時，需補充欄位參數
        [HttpPost]
        public ActionResult Edit(Member _form) 
        {
            //var editlist = db.Member.Where(m => m.UserID == UserID).FirstOrDefault();
            //editlist.UserID = _form.UserID;
            //editlist.UserAccount = _form.UserAccount;
            //editlist.UsePass = _form.;
            //editlist.UserName = UserName;
            //editlist.UserPhone = Phone;
            //editlist.UserSex = UserSex;
            //editlist.City = UserCity;
            //editlist.Region = UserRegion;
            //editlist.Adress  = Address;
            //editlist.EMail = Email;
            db.Entry(_form).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //刪除會員資料
        public ActionResult Delete(int id) 
        {
            var del = db.Member.Where(m => m.UserID == id).FirstOrDefault();
            db.Member.Remove(del);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //新增會員資料
        public ActionResult Create()
        {
            return View();
        }

        //新增會員資料
        [HttpPost]
        public ActionResult Create(Member createList) 
        {
            db.Member.Add(createList);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}