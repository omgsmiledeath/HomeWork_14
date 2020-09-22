using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HomeWork_13.Models;

namespace HomeWork_13.ViewModels
{
    public class BusinessViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<Business> Clients { get; set; }
        
        public BusinessViewModel(ObservableCollection<Business> businesses)
        {
            this.Clients = businesses;
        }

        public void addClient(string name, string adress, string phone,string director,string type)
        {
            Clients.Add(new Business(name, adress, phone,director,type));
            OnPropertyChanged("AddClient");
        }

        public void delClient(Business client)
        {
            Clients.Remove(client);
            OnPropertyChanged("DelClient");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
