using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votp.Tokens.Abstractions.Models;

namespace Votp.Tokens.Totp.Models
{
    public class TotpTokenODto : TokenODto
    {
        public string QRImageBase64 { get; set; }
        public string KeyBase32 { get; set; }
        public int Step { get; set; }
        public int TokenSize { get; set; }
    }
}
