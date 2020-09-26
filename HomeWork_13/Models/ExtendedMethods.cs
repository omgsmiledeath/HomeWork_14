using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankClassLibrary;
namespace HomeWork_14.Models
{
    public static class ExtendedMethods
    {
        public static bool FullCheckToDouble(this string s,out double d)
        {
            if (!String.IsNullOrWhiteSpace(s))
            {
                if (Double.TryParse(s, out d))
                    return true;
                else
                    throw new FullCheckException("Неверное значение");
            }
            else throw new FullCheckException("Заполните данные");
        }

        public static bool FullCheckToByte(this string s, out byte b)
        {
            if (!String.IsNullOrWhiteSpace(s))
            {
                if (Byte.TryParse(s, out b))
                    return true;
                else
                    throw new FullCheckException("Неверное значение");
            }
            else throw new FullCheckException("Заполните данные");
        }


        public class FullCheckException : Exception
        {
            public FullCheckException(string msg) 
                :base(msg)
            {

            }
        }
    }
}
