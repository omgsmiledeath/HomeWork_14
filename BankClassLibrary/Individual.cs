using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankClassLibrary
{
    public class Individual : Client
    {
        public Individual() : base()
        {

        }
        public Individual(string name, string addr, string phone) : base(name, addr, phone)
        {
            loyality = 10;
        }

    }
}
