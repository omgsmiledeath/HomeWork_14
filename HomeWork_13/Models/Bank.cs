using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_13.Models
{
    public class Bank<T>
        where T : Client
    {
        public Bank()
        {
            
        }

        public Bank (ObservableCollection<T> clients)
        {
            clientList = clients;
        }

        private ObservableCollection<T> clientList = new ObservableCollection<T>();

        public ObservableCollection<T> ClientList { get => clientList; set => clientList = value; }

        public void AddClient(T client)
        {
            clientList.Add(client);
        }
    }
}
