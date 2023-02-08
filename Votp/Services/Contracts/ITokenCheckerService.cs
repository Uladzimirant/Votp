namespace Votp.Services.Contracts
{
    public interface ITokenCheckerService
    {
        public Task<bool> Check(string userm, string token);
    }
}
