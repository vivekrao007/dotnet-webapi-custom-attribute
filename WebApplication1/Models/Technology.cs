using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Technology: DataModification
    {
        public int TechnologyId { get; set; }
        [Required(ErrorMessage = "Technology Name is required.")]
        [MaxLength(50, ErrorMessage = "Technology Name cannot be more than 50 characters")]
        public string TechnologyName { get; set; }
        public int IsDeleted { get; set; }
    }
}