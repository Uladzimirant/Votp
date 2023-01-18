namespace Votp.DS.Database.Entities
{
    public class Token
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public User User { get; set; }
        public DateTime? RegistrationTime { get; set; }
    }
}