using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MassAccountManagementSystem.Models;

namespace MassAccountManagementSystem.Controllers
{
    [Authorize(Users = "Admin , User")]
    public class ProfileController : Controller
    {
        
        //
        // GET: /Profile/
        MassAccountDBContext db=new MassAccountDBContext();
        [AllowAnonymous]
        public ActionResult Index(int id)
        {
            var user = db.Users.SingleOrDefault(a => a.UserId == id);
            var userdata = db.Meals.Where(a => a.UserId == id).ToList();
            
            Session["userdata"] = userdata;

            var allmeal = db.Meals.Sum(a => a.Meal1);
            var allamount = db.Meals.Sum(a => a.Amount);
            var parmeal = allamount/allmeal;
            Session["parmeal1"] = parmeal;
            RegisterUser u=new RegisterUser();
            

            return View(user);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = db.Users.Find(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            string fileName = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
            string extention = Path.GetExtension(user.ImageFile.FileName);
            fileName = fileName + extention;
            user.ImagePath = "~/Images/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
            user.ImageFile.SaveAs(fileName);
            db.Entry(user).State=EntityState.Modified;
            db.SaveChanges();
            
            return View();
        }
	}
}