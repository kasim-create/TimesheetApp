using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TimesheetApp.Models.EntityModels
{
    public class Timesheet
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Project { get; set; }

        public string DescriptionOfTasks { get; set; }

        public int HoursWorked { get; set; }
    }
}
