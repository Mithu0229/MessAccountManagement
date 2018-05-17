using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MassAccountManagementSystem.Models;

namespace MassAccountManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        MassAccountDBContext db=new MassAccountDBContext();
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult QuestionForPass(string question,string UserName)
        {
            if (question!=String.Empty && UserName!=String.Empty)
            {
                if (!db.Users.Any(a => a.Question == question && a.UserName == UserName))
                {
                    return Json("Please enter question and userName");
                }
                var password = db.Users.FirstOrDefault(a => a.Question == question && a.UserName == UserName).Password;
                
                return Json("Your password is " + password);
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Login(Login login,string url)
        {
            var authenticate = db.Users.Any(a => a.UserName == login.UserName && a.Password == login.Password);
           
            if (authenticate)
            {
                var role = db.Users.FirstOrDefault(a => a.UserName == login.UserName).Role;
                var userId = db.Users.FirstOrDefault(a => a.UserName == login.UserName).UserId;
                Session["id"] = userId;
               
                if (role == "Admin")
                {
                    FormsAuthentication.SetAuthCookie(role,false);
                    return Redirect(url ?? Url.Action("Index", "Profile", new { @id = userId }));
                }
                else if (role == "User")
                {
                    FormsAuthentication.SetAuthCookie(role, false);
                    return Redirect(url ?? Url.Action("Index", "Profile", new { @id = userId }));
                }

               
            }
            
            return View("Index");
        }


        public ActionResult RegisterAccount()
        {
            return View();
        }

        [HttpPost]
       
        public ActionResult RegisterAccount(RegisterUser register)
        {
            register.Role = "User";
            Models.User user=new User();
            user.Name = register.Name;
            user.UserName = register.UserName;
            user.Address = register.Address;
            user.Email = register.Email;
            user.Phone = register.Phone;
            user.Password = register.Password;
            user.Role = register.Role;
            user.Question = register.Question;
            string fileName = Path.GetFileNameWithoutExtension(register.ImageFile.FileName);
            string extention = Path.GetExtension(register.ImageFile.FileName);
            fileName = fileName + extention;
            register.ImagePath = "~/Images/" + fileName;
            user.ImagePath = register.ImagePath;
            fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
            register.ImageFile.SaveAs(fileName);
           // user.ImagePath = fileName;
            db.Users.Add(user);
            
            db.SaveChanges();

            return View("Index");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            return View("Index");
        }
       
	}
}