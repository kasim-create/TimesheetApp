using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TimesheetApp.Models
{
    public class TimesheetViewModel
    {
        public int? Id { get; set; } 

        [Display(Name = "Employee Name"), Required(ErrorMessage = "Name must not be empty")]
        public string Name { get; set; }

        [Display(Name = "Date Worked"), Required(ErrorMessage = "Date must not be empty")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Project must not be empty")]
        public string Project { get; set; }

        [Display(Name = "Description of Tasks"), Required(ErrorMessage = "Description must not be empty")]
        public string DescriptionOfTasks { get; set; }

        [Display(Name = "Hours Worked"), Required(ErrorMessage = "Hours worked must not be empty")]
        [Range(0, 24)]
        public int HoursWorked { get; set; }

        public int? TotalHoursWorked { get; set; }

    }
}
