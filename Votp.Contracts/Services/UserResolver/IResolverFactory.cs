namespace Votp.Contracts.Services.UserResolver
{
    public interface IResolverFactory<T>
    {
        IResolver<T> CreateResolver(IResolverInfo<T> info);
    }
}
