using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace DataModels.Models
{
    public class Employee
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Joining Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime JoiningDate { get; set; }

        [Range(22, 60)]
        public int Age { get; set; }
    }

    public class EmpDBContext : DbContext
    {
        public EmpDBContext()
        { }
        public DbSet<Employee> Employees { get; set; }
    }
}