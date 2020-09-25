using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions
{
    public class AccountException : Exception
    {
        public enum AccountExceptionTypes
        {
            NegativeValue
        }

        public AccountExceptionTypes Type;

        public AccountException (string msg,AccountExceptionTypes type) 
            :base(msg)
        {
            this.Type = type;
        }
    }
}
