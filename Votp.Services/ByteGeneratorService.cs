using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votp.Contracts.Services;

namespace Votp.Services
{
    public class ByteGeneratorService : IByteGeneratorService
    {
        Random r = new Random();

        public byte[] Generate(int size)
        {
            var arr = new byte[size];
            r.NextBytes(arr);
            return arr;
        }
    }
}
