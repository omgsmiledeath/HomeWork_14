
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BankClassLibrary
{
    public class CreditAccount : Account
    {
        private double limit;
        private double creditBalance;
        private double creditRate;
        private delegate void CreditLogDelegate(string s);
        private CreditLogDelegate CreditLog;
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
            CreditLog = AddLog;
            CreditLog?.Invoke($"Created credit cart : {CartNumber}");
        }

        public bool GetCredit(double amount)
        {
            if(CreditBalance+amount<=Limit)
            {
                CreditBalance += amount+amount*creditRate/100;
                Deposit(amount);
                CreditLog?.Invoke($"Get credit at {amount} your debt {CreditBalance}");
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
                CreditLog?.Invoke($"Close credit at {amount}");
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