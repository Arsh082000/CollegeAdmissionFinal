using System.ComponentModel.DataAnnotations;

namespace CollegeAdmission.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Display(Name = "Course Name ")]
        public string CourseName { get; set; }

        [Display(Name = "Grade ")]
        public string Grade { get; set; }


        [Display(Name = "Duration ")]
        public string Duration { get; set; }


    }
}
