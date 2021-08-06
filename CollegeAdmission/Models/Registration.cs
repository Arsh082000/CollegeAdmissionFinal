using System.ComponentModel.DataAnnotations;

namespace CollegeAdmission.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Father Name")]
        public string FatherName { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }
        
        [Display(Name = "Qualification")]
        public string Qualification { get; set; }
        
        [Display(Name = "CourseId")]
        public int CourseId { get; set; }

        [Display(Name = "Contact")]
        public string Contact { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }
    }
}
