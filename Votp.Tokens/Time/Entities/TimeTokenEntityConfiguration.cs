using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Votp.DS.Entities;
using Votp.Tokens.Abstractions;
using Votp.Tokens.Totp.Entities;

namespace Votp.Tokens.Time.Entities
{
    public class TimeTokenEntityConfiguration : TokenEntityConfiguration<TimeToken>
    {
    }
}
