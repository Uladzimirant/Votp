using System.Reflection;

namespace Votp.Contracts.Services
{
    public interface IDBLibService
    {
        ICollection<Assembly> LibAssemblies { get; }
    }
}
