using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcIng.Models
{
    public class PasswordModel
    {
        [Required(ErrorMessage="Please enter your old password")]
        public string oldPassword { get; set; }
        [Required(ErrorMessage="Please enter your new password")]
        public string newPassword { get; set; }
    }
}