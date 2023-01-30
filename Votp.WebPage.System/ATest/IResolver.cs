namespace Votp.ATest
{
    public interface IResolver<T>
    {
        IEnumerable<T> ListResolved();
    }
}
