﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankClassLibrary
{
    public class VipClient : Client
    {
        public VipClient(): base()
        {

        }
        public VipClient(string name, string addr, string phone) : base(name, addr, phone)
        {
            loyality = 100;
        }

        

    }
}
