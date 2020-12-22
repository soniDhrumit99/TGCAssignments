using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SourceControlFInalAssignment.Models;

namespace SourceControlFInalAssignment.Context
{
    public class UserContext : DbContext
    {
        public UserContext() : base("name=UserContext") { }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<LoginModel> Logins { get; set; }
    }
}