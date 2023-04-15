using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votp.Tokens.Abstractions.Models;

namespace Votp.Tokens.Totp.Models
{
    public class TotpTokenIDto : TokenIDto
    {
        public int Step { get; set; } = 30;
        public int TokenSize { get; set; } = 6;

    }
}
