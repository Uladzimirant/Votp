using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votp.DS.Entities;

namespace Votp.Contracts
{
    public interface IInnerUsersDBContext : IDBContext
    {
        DbSet<User> Users { get; }
    }
}
