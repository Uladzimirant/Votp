using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Votp.Contracts.Services
{
    public interface IByteGeneratorService
    {
        byte[] Generate(int size);
    }
}
