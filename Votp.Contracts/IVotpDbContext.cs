using Microsoft.EntityFrameworkCore;
using Votp.DS.Entities;

namespace Votp.Contracts
{
    public interface IVotpDbContext
    {
        public DbSet<ResolverInfo> Resolvers { get; }
        public DbSet<Token> Tokens { get; }
        public DbSet<User> Users { get; }
        DbContext AsContext() { return (DbContext)this; }
    }
}
