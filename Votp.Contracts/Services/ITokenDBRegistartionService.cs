using System.Reflection;

namespace Votp.Contracts.Services
{
    public interface ITokenLibService
    {
        ICollection<Assembly> TokenLibAssemblies { get; }
    }
}
