using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MassAccountManagementSystem.Models
{
    public class RegisterUser
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
         [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ImagePath { get; set; }
         [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
         [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
         [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string Role { get; set; }
         [Required(ErrorMessage = "Question is required")]
        public string Question { get; set; }

         public HttpPostedFileBase ImageFile { get; set; }
    }
}