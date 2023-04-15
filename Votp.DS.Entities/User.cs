using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votp.DS.Entities;

namespace Votp.DS.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public List<Token> Tokens { get; set; }
    }
}
