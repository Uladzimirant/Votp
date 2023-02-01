using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votp.DS.Database.Entities;

namespace Votp.DS.Database
{
    public interface IVotpDbContext
    {
        public DbSet<ResolverInfo> Resolvers { get; }
        public DbSet<Token> Tokens { get; }
        public DbSet<User> Users { get; }
        int SaveChanges();
    }
}
