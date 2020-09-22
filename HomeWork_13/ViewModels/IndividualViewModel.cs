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
    public class IndividualViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Individual> Clients { get; set; }
        
        public IndividualViewModel(ObservableCollection<Individual> individuals)
        {
            this.Clients = individuals;
        }

        public void addClient()
        {
            Clients.Add(new Individual());
            OnPropertyChanged("AddClient");
        }
        
        public void delClient(Individual client)
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
