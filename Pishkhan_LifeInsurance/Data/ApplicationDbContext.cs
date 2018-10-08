using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pishkhan_LifeInsurance.Models;
using Pishkhan_LifeInsurance.Data.DataBase;

namespace Pishkhan_LifeInsurance.Data
{
    public class ApplicationDbContext : IdentityDbContext<TblUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //Add Index
            builder.Entity<TblPishkhan>().HasIndex(p => new { p.ID }).IsUnique(true);
        }


        //Add Tabels
        public DbSet<TblPishkhan> TblPishkhan { get; set; }
        public DbSet<TblSupervisior> TblSupervisior { get; set; }
        public DbSet<TblLifeInsurance> TblLifeInsurance { get; set; }
        public DbSet<TblSetting> TblSetting { get; set; }
        public DbSet<TblKarmozd> TblKarmozd { get; set; }
    }
}
