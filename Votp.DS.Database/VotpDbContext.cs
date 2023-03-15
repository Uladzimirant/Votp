using Microsoft.EntityFrameworkCore;
using Votp.Contracts.Services;
using Votp.DS.Database.Entities;
using Votp.Utils;
using Votp.DS.TToken;

namespace Votp.DS.Database
{
    public class VotpDbContext : DbContext, IVotpDbContext
    {
        private ITokenLibService _tokenLibs;
        public VotpDbContext(DbContextOptions o, ITokenLibService tokenLibs) : base(o)
        {
            _tokenLibs = tokenLibs;
            Database.EnsureCreated();
        }
        public virtual DbSet<ResolverInfo> Resolvers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }

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

                ent.HasMany(u => u.Tokens).WithOne(t => t.User)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            foreach (var assembly in _tokenLibs.TokenLibAssemblies)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            }

            modelBuilder.Entity<Token>(ent =>
            {
                ent.ToTable("Token");
                ent.HasKey(ent => ent.Id);
                ent.Property(e => e.Id);
                ent.Property(e => e.Value)
                .IsRequired();
            });

            modelBuilder.Entity<ResolverInfo>(ent =>
            {
                ent.HasKey(e => e.Id);
            });

        }
    }
}