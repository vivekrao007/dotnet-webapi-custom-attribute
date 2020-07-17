using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Roles
    {
        public int RoleId { get; set; }
        [Required(ErrorMessage = "Role Name is required.")]
        public string RoleName { get; set; }
    }
}