using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votp.DS.Entities;
using Votp.Tokens.Time.Entities;
using Votp.Tokens.Totp.Entities;
using Votp.Tokens.Abstractions;

namespace Votp.Tokens.Totp
{
    public class TotpEntityConfiguration : TokenEntityConfiguration<TotpToken>
    {
    }
}
