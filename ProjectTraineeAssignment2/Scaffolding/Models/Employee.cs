using System;
using System.Data.Entity;

namespace MVCScaffoldingDemo.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime JoiningDate { get; set; }
        public int Age { get; set; }
    }

    public class EmpDBContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
    }
}