using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votp.Contracts;
using Votp.Contracts.Services;
using Votp.DS.Entities;

namespace Votp.DS.Database
{
    public class InnerUsersDBContext : DbContext, IInnerUsersDBContext
    {
        public InnerUsersDBContext(DbContextOptions<InnerUsersDBContext> o) : base(o)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(ent =>
            {
                ent.ToTable("User");
                ent.HasKey(ent => ent.Id);

                ent.Property(e => e.Id);
                ent.Property(e => e.Login)
                .HasMaxLength(40)
                .IsRequired();

            });
        }
    }
}
