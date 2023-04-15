using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Votp.DS.Database.Entities;

namespace Votp.DS.TToken
{
    public class TimeTokenEntityConfiguration : IEntityTypeConfiguration<TimeToken>
    {
        public void Configure(EntityTypeBuilder<TimeToken> builder)
        {
            builder.HasBaseType<Token>();
        }
    }
}
