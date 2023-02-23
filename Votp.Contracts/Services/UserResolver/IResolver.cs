namespace Votp.Contracts.Services.UserResolver
{
    public interface IResolver<T>
    {
        IEnumerable<T> GetResolvedList();
    }
}
