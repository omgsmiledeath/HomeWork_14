using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_13.Models
{
    public abstract class Client :INotifyPropertyChanged ,IEquatable<Client>

    {
        static long id_count;

        static Client()
        {
            id_count=0;
        }

        private long id;
        private string client_full_name;
        private string address;
        private string phone_number;
        protected double loyality;
        private ObservableCollection<Account> carts;

        public event PropertyChangedEventHandler PropertyChanged;//Евент изменения или добавления полей
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public long Id { get => id; }
        public string Client_full_name { get => client_full_name; set 
            { 
                client_full_name = value;
                OnPropertyChanged("FullName");
            } 
        }
        public string Address { get => address; set 
            { 
                address = value;
                OnPropertyChanged("Adress");
            } 
        }
        public string Phone_number { get => phone_number; set 
            {
                phone_number = value;
                OnPropertyChanged("Phone");
            }
        }
        public ObservableCollection<Account> Carts { get => carts;  }

        public Client()
        {
            id = ++id_count;
            loyality = 10;
            carts = new ObservableCollection<Account>();
        }
        public Client(string name,string addr,string phone)
        {
            Client_full_name = name;
            Address = addr;
            Phone_number = phone;
            id = ++id_count;
            loyality = 10;
            carts = new ObservableCollection<Account>();
        }

        public bool CheckAndOpenAccount(Account.AccountTypes type,double amount,double limit)
        {
            if (type == Account.AccountTypes.Debit)
            {
                foreach (var el in Carts)
                {
                    if (el is SaveAccount) return false;
                }
                addDebitCart(amount);
                return true;
            }
            else
            {
                foreach (var el in Carts)
                {
                    if (el is CreditAccount) return false;
                }
                addCreditCart(amount, limit);
                return true;
            }
     
        }

        private void addDebitCart(double amount)
        {
            if(loyality==100)
                carts.Add(new SaveAccount(amount,4));
            else
                carts.Add(new SaveAccount(amount));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SaveAccount)));
        }

        private void addCreditCart(double amount, double limit)
        {
            if (loyality == 100)
                carts.Add(new CreditAccount(amount, limit, 3));
            else
                carts.Add(new CreditAccount(amount, limit));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CreditAccount)));
        }

        bool IEquatable<Client>.Equals(Client other)
        {
            if (this.Id == other.Id) return true;
            return false;
        }
    }



    
}
