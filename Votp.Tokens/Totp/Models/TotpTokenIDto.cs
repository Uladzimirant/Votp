using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votp.Tokens.Abstractions.Models;

namespace Votp.Tokens.Totp.Models
{
    public class TotpTokenIDto : TokenIDto
    {
        [ReadOnly(true)]
        public int Step { get; private set; } = 30;
        [ReadOnly(true)]
        public int TokenSize { get; private set; } = 6;

    }
}
