using System.ComponentModel.DataAnnotations.Schema;

namespace Votp.DS.Database.Entities
{
    public class Token
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public User User { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? RegistrationTime { get; set; }
        public virtual bool CheckCode(string code)
        {
            return false;
        }
        public virtual bool Check(string code)
        {
            return CheckCode(code);
        }
    }
}