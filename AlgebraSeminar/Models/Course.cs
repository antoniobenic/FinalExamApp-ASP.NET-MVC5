using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlgebraSeminar.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage ="Name of course must be less than 100 characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name= "Date of beginning of the course")]
        public DateTime DateOfBeginning { get; set; }

        [Required]
        [Range(minimum: 1, maximum: 150, ErrorMessage ="Number of students must be between 1 and 150")]
        [Display(Name="Maximum number of students for that course")]
        public int MaxNumberOfStudents { get; set; }

        [Display(Name="Current number of pre-registrated students")]
        public int CurrentNumberOfStudents { get;  set; }

        public bool Filled { get; set; }

        public virtual IEnumerable<Pre_Registration> Pre_Registration { get; set; }
    }
}