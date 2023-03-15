namespace Votp.Models.Response
{
    public class TokenODto
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string TokenType { get; set; }
        public string UserName { get; set; }
        public DateTime? RegistrationTime { get; set; }
    }
}
