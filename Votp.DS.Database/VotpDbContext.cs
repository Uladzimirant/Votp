using Microsoft.EntityFrameworkCore;
using Votp.DS.Database.Entities;
using Votp.Utils;

namespace Votp.DS.Database
{
    public class VotpDbContext : DbContext, IVotpDbContext
    {
        public VotpDbContext(DbContextOptions o) : base(o)
        {
            if (Database.EnsureCreated())
            {
                var r = Randomizer.Instance;
                var users = Enumerable.Range(0, 5)
                    .Select(i => new User()
                    {
                        Login = r.NextWord(5),
                        Tokens =
                        Enumerable.Range(1, r.Next(1, 3))
                        .Select(i => new Token() { Value = r.NextAlphaNum(3), RegistrationTime = DateTime.Now }).ToList()
                    }
                    ).ToList();
                Users!.AddRange(users);

                SaveChanges();
            }
        }
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

            modelBuilder.Entity<Token>(ent =>
            {
                ent.ToTable("Token");
                ent.HasKey(ent => ent.Id);

                ent.Property(e => e.Id);
                ent.Property(e => e.Value)
                .IsRequired();
            });
        }
    }
}