using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZRada.Models;

namespace ZRada.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Deputat> Deputats { get; set; }
        public virtual DbSet<Vote> Votes { get; set; }
        public virtual DbSet<Voting> Votings { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=tcp:95.67.19.2,42424;Initial Catalog=Jazzoilique;Persist Security Info=False;User ID=sa;Password=ZZzz1234;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True;Connection Timeout=300;",
                    sqlServerOptionsAction: options => { options.EnableRetryOnFailure(); options.CommandTimeout(6000); });

                // optionsBuilder.UseSqlServer("Server=tcp:95.67.19.2,42424;Initial Catalog=JazzoilSalesDebug;Persist Security Info=False;User ID=sa;Password=ZZzz1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Deputat>(
                entity =>
                {
                    entity.HasIndex(e => new { e.Name})
                    .HasName("IX_UniqueRecord")
                    .IsUnique();                    
                });

            builder.Entity<Vote>()
            .HasIndex(b => new { b.Value, b.Date });
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
