using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModels.Models;

namespace DatabaseAccess.Context
{
    class AppDbContext : DbContext
    {
        public DbSet<Hotels> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
    }
}
