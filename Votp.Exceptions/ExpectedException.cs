using System.Runtime.Serialization;

namespace Votp.Exceptions
{
    public class ExpectedException : Exception
    {
        private const string DefaultType = "STANDART";
        private string _type = DefaultType;

        public ExpectedException()
        {
        }

        public ExpectedException(string? message, string type = DefaultType) : base(message)
        {
            _type = type;
        }

        public ExpectedException(string? message, Exception? innerException) : this(message, DefaultType, innerException)
        {
        }

        public ExpectedException(string? message, string type, Exception? innerException) : base(message, innerException)
        {
            _type = type;
        }
        protected ExpectedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string Type { get => _type; set
            {
                _type = value.ToUpper();
            } }
    }
}