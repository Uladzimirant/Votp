namespace Votp.Services.Contracts
{
    public interface ITokenCheckerService
    {
        public bool Check(string userm, string token);
    }
}
