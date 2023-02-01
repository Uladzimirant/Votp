namespace Votp.Services.Contracts.UserResolver
{
    public interface IResolverFactory<T>
    {
        IResolver<T> CreateResolver(IResolverInfo<T> info);
    }
}
