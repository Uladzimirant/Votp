using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votp.DS.Entities;

namespace Votp.Tokens.Abstractions
{
    public abstract class TokenEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasBaseType<Token>();
        }
    }
}
