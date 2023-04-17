using Microsoft.EntityFrameworkCore;
using Votp.Contracts.Services;
using Votp.Contracts;
using Votp.UserResolver.Ldap;
using Votp.DS.Entities;
using Votp.UserResolver.InnerDatabase;

namespace Votp.DS.Database
{
    public class VotpDbContext : DbContext, IVotpDbContext
    {
        private IDBLibService _libs;
        public VotpDbContext(DbContextOptions<VotpDbContext> o, IDBLibService libs) : base(o)
        {
            _libs = libs;
        }
        public virtual DbSet<ResolverInfo> Resolvers { get; set; }
        //public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>(ent =>
            //{
            //    ent.ToTable("User");
            //    ent.HasKey(ent => ent.Id);

            //    ent.Property(e => e.Id);
            //    ent.Property(e => e.Login)
            //    .HasMaxLength(40)
            //    .IsRequired();

            //    ent.HasMany(u => u.Tokens).WithOne(t => t.User)
            //    .OnDelete(DeleteBehavior.ClientSetNull);
            //});

            modelBuilder.Entity<Token>(ent =>
            {
                ent.ToTable("Token");
                ent.HasKey(ent => ent.Id);
                ent.Property(e => e.Id);
                ent.Ignore(e => e.User);
            });

            modelBuilder.Entity<ResolverInfo>(ent =>
            {
                ent.HasKey(e => e.Id);
            });
            modelBuilder.Entity<DatabaseUserResolverInfo>(ent =>
            {
                ent.HasBaseType<ResolverInfo>();
            });
            modelBuilder.Entity<LdapUserResolverInfo>(ent =>
            {
                ent.HasBaseType<ResolverInfo>();
            });

            foreach (var assembly in _libs.LibAssemblies)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            }
        }
    }
}