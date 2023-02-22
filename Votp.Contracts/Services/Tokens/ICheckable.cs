namespace Votp.Contracts.Services.Tokens
{
    public interface ICheckable
    {
        public bool Check(string code);
    }
}
