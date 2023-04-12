using Microsoft.EntityFrameworkCore;
using Votp.DS.Database.Entities;

namespace Votp.DS.Database
{
    public interface IVotpDbContext
    {
        public DbSet<ResolverInfo> Resolvers { get; }
        public DbSet<Token> Tokens { get; }
        public DbSet<User> Users { get; }
        DbContext AsContext() { return (DbContext)this; }
    }
}
