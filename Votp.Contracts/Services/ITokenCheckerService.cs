namespace Votp.Contracts.Services
{
    public interface ITokenCheckerService
    {
        public Task<bool> Check(string userm, string token);
    }
}
