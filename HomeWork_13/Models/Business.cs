using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_13.Models
{
    public class Business : Client
    {
        private string companyDirector;
        private string type;
        public string CompanyDirector { get => companyDirector; set => companyDirector = value; }
        public string Type { get => type; set => type = value; }


        public Business() :base()
        {
        
        }
        public Business(string name, string addr, string phone,string director,string type) : base(name, addr, phone)
        {
            loyality = 20;
            CompanyDirector = director;
            Type = type;
        }

        
    }
}
