using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MassAccountManagementSystem.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Enter a valid UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter a valid Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Remember { get; set; }

    }
}