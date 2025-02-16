using Microsoft.EntityFrameworkCore;
using TimesheetApp.Models.EntityModels;

namespace TimesheetApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Timesheet> Timesheets { get; set; }
    }
 }
