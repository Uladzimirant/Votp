using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Votp.Contracts.Services;
using Votp.DS.TToken;

namespace Votp
{
    public class RegistratorService : ITokenLibService
    {
        public ICollection<Assembly> TokenLibAssemblies { get; set; } = new List<Assembly>();
    }
}
