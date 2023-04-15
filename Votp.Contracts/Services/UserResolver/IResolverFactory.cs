using Votp.DS.Entities;

namespace Votp.Contracts.Services.UserResolver
{
    public interface IResolverFactory<T>
    {
        IResolver<T> CreateResolver(ResolverInfo info);
    }
}
