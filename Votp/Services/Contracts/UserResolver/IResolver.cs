namespace Votp.Services.Contracts.UserResolver
{
    public interface IResolver<T>
    {
        IEnumerable<T> GetResolvedList();
    }
}
