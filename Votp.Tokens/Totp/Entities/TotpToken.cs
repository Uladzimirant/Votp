using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votp.DS.Entities;

namespace Votp.Tokens.Totp.Entities
{
    public class TotpToken : Token
    {
        public byte[] Key { get; set; } = new byte[0];
        public int Step { get; set; } = 30;
        public int TokenSize { get; set; } = 6;

        public override bool CheckCodeRaw(string code)
        {
            var totp = new OtpNet.Totp(Key, step: Step, totpSize: TokenSize);
            bool verified = totp.VerifyTotp(code, out long timeWindowUsed);
            return verified;
        }
    }
}
