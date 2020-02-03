using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlgebraSeminar.Models
{
    public class Pre_Registration
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="First name")]
        [StringLength(25,ErrorMessage ="Fist name must be less than 25 characters")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name ="Last name")]
        [StringLength(25, ErrorMessage = "Last name must be less than 25 characters")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(254,ErrorMessage ="Email adress must be less than 254 characters")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(20, ErrorMessage ="Phone number must be less than 20 characters")]
        [Display(Name ="Phone number")]
        public string PhoneNumber { get; set; }

        public bool? Status { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pick the course you want to pre-regisration it")]
        [Display(Name ="Course")]
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}