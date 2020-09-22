
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HomeWork_13.Models
{
    public class CreditAccount : Account
    {
        private double limit;
        private double creditBalance;
        private double creditRate;

        public double Limit {
            get => limit;
             
        }

        public double CreditBalance { get => creditBalance; set
            {
                creditBalance = value;
                OnPropertyChanged("CreditBalance");
            }
        }

        public CreditAccount(double amount,double limit,double creditRate = 10) : base(amount,AccountTypes.Credit)
        {
            this.limit = limit;
            this.creditRate = creditRate;
            CreditBalance = 0;
            
        }

        public bool GetCredit(double amount)
        {
            if(CreditBalance+amount<=Limit)
            {
                CreditBalance += amount+amount*creditRate/100;
                Balance += amount;
                LogTransaction.Add($"Get credit at {amount}");
                return true;
            }
            return false;
        }

        public bool CloseCredit(double amount)
        {
            if ((Balance - amount >= 0) && (CreditBalance-amount>=0))
            {
                CreditBalance -= amount;
                Balance -= amount;
                LogTransaction.Add($"Close credit at {amount}");
                if (CreditBalance <= 0)
                {
                    Balance += Math.Abs(creditBalance);
                    creditBalance = 0;
                    creditRate -= 0.2;
                }
                return true;
            }
            return false;
        }



    }
}