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
    public class VipViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<VipClient> Clients { get; set; }
      
        public VipViewModel(ObservableCollection<VipClient> vipClients)
        {
            this.Clients = vipClients;
        }

        public void addClient(string name, string adress, string phone)
        {
            Clients.Add(new VipClient(name, adress, phone));
            OnPropertyChanged("AddClient");
        }

        public void delClient(VipClient client)
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
