using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class MeadowLogContext : DbContext
    {
        public MeadowLogContext(DbContextOptions<MeadowLogContext> options)
            : base(options)
        {
        }

        public DbSet<MeadowLog> MeadowLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeadowLog>(entity =>
            {
                entity.ToTable("meadowlogs");
                entity.HasKey(e => e.MeadowLogID);
                entity.HasIndex(e => e.MeadowLogID);
                entity.Property(e => e.MeadowLogID).HasColumnName("meadowlogid");
                entity.Property(e => e.LogData).HasColumnName("logdata");
            });


        }


    }
}
