using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MassAccountManagementSystem.Models;

namespace MassAccountManagementSystem.Controllers
{
    [Authorize]
    public class MealController : Controller
    {
        //
        // GET: /Meal/
        MassAccountDBContext db=new MassAccountDBContext();
        public ActionResult Index()
        {
            //all meal calculate
            return View(db.Meals.ToList());
        }

        public ActionResult AddMeal()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMeal(MealAdd meal)
        {// add meal
            Meal mealdb=new Meal();
            mealdb.Meal1 = meal.Meal1;
            mealdb.Amount = meal.Amount;
            mealdb.UserId = (int) Session["id"];
            mealdb.Date=DateTime.Now;
            db.Meals.Add(mealdb);
            db.SaveChanges();
             var totalMyMeal = db.Meals.Where(a => a.UserId == mealdb.UserId).Sum(a => a.Meal1);
             var totalMyAmount = db.Meals.Where(a => a.UserId == mealdb.UserId).Sum(a => a.Amount);
            Session["totalMyMeal"] = totalMyMeal;
            Session["totalMyAmount"] = totalMyAmount;

            var totalMeal = db.Meals.Sum(a => a.Meal1);
            var totalAmount = db.Meals.Sum(a => a.Amount);
            Session["totalMeal"] = totalMeal;
            Session["totalAmount"] = totalAmount;

            var parmeal = totalAmount/totalMeal;
            var totalmealParmeal = parmeal*totalMyMeal;
            Session["parmeal"] = parmeal;
            Session["totalmealParmeal"] = totalmealParmeal;

            if (totalMyAmount >= totalmealParmeal)
            {
                var getAmount = totalMyAmount - totalmealParmeal;
                Session["getAmount"] = getAmount;
            }
            else if (totalMyAmount <= totalmealParmeal)
            {
                var giveamount = totalmealParmeal - totalMyAmount;
                Session["giveamount"] = giveamount;
            }

            return RedirectToAction("OwnMeal", "Meal", new {@id = (int)Session["id"]});

            
        }

        public ActionResult OwnMeal(int id)
        {
            var meal = db.Meals.Where(a => a.UserId == id).ToList();
            Session["meal"] = meal;
            
            return View(id);
        }

        public ActionResult CalculateMealAndAmount()
        {
          
            return View(db.Meals.ToList());
        }

        public ActionResult CalculateMealAndAmountPdf()
        {
            return new Rotativa.MVC.ActionAsPdf("CalculateMealAndAmount");
        }


	}
}