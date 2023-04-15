using System.ComponentModel.DataAnnotations.Schema;

namespace Votp.DS.Entities
{
    public class Token
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public User User { get; set; }
        public bool? Locked { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? RegistrationTime { get; set; }

        public string? Prefix { get; set; }

        public virtual bool CheckCodeRaw(string code)
        {
            return false;
        }
        public virtual bool Check(string code)
        {
            var prefix = Prefix ?? string.Empty;
            var prefixCode = code.Substring(0, prefix.Length);
            var rawCode = code.Substring(prefix.Length);
            return !(Locked ?? false) && prefix.Equals(prefixCode) && CheckCodeRaw(rawCode);
        }
    }
}