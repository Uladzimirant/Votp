using Votp.DS.Entities;

namespace Votp.Token.TimeToken.Entities
{

    public class TimeToken : Token
    {
        public string Prefix { get; set; }

        public override bool CheckCodeRaw(string code)
        {
            var d = DateTime.Now;
            return Prefix + d.Hour.ToString() + d.Minute.ToString() == code;
        }
    }
}