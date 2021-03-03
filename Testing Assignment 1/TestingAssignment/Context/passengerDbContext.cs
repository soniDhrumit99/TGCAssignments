using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TestingAssignment.Models;

namespace TestingAssignment.Context
{
    public class passengerDbContext : DbContext
    {
        public passengerDbContext() : base("name=passengerDbContext") {}
        public DbSet<Passenger> Passengers { set; get; }
    }
}