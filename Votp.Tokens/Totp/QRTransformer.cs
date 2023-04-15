using AutoMapper;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Votp.DS.Entities;

namespace Votp.Tokens.Totp
{
    public static class StringToBase64QREncoder
    {

        public static string Transform(string stringToEncode, int qrSize = 5)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrData = qrGenerator.CreateQrCode(stringToEncode, QRCodeGenerator.ECCLevel.Q);
            var qr = new BitmapByteQRCode(qrData);
            return Convert.ToBase64String(qr.GetGraphic(qrSize));
        }
    }
}
