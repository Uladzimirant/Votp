using Votp.DS.Entities;

namespace Votp.Tokens.Time.Entities
{
    public class TimeToken : Token
    {
        public override bool CheckCodeRaw(string code)
        {
            var d = DateTime.Now;
            return d.Hour.ToString() + d.Minute.ToString() == code;
        }
    }
}