using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MassAccountManagementSystem.Models
{
    public class MealAdd
    {
        public int MealId { get; set; }
        //[Required(ErrorMessage = "Enter your Meal.")]
        //[RegularExpression(@"\d",ErrorMessage = "Enter a mumber")]
        public int Meal1 { get; set; }
        //[Required(ErrorMessage = "Enter your Amount.")]
        //[RegularExpression(@"\d", ErrorMessage = "Enter a mumber")]
        public int Amount { get; set; }
        public int UserId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }

    }
}