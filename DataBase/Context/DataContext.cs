using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Context
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            //optionsBuilder.UseSqlServer
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {


        }
    }
}
