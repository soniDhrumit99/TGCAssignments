using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SourceControlFinalAssignment.Models;

namespace SourceControlFinalAssignment.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("ApplicationContext"){ }

        public DbSet<Login> Logins { get; set; }
        public DbSet<User> Users { get; set; }

        public System.Data.Entity.DbSet<SourceControlFinalAssignment.Models.UserRegistrationViewModel> UserRegistrationViewModels { get; set; }
    }
}