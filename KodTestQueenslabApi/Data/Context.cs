using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KodTestQueenslabApi.Models;

namespace KodTestQueenslabApi.Data
{
    public class Context : DbContext
    {
        public Context( DbContextOptions options) : base(options)
        {
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
    }
}
