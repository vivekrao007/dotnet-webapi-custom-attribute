using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class User: DataModification
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "UserName is required.")]
        [MaxLength(20,ErrorMessage = "username cannot be more than 20 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(5,ErrorMessage = "Password should be more than 5 characters")]
        [MaxLength(20, ErrorMessage = "Password cannot be more than 20 characters")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Phone Number is required.")]
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is Invalid")]
        [MaxLength(50, ErrorMessage = "Email cannot be more than 50 characters")]
        public string Email { get; set; }
        public int RoleId { get; set; }
        public List<int> TechIds { get; set; }
        public int IsDeleted { get; set; }
    }
}