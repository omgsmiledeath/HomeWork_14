using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BankClassLibrary;
namespace HomeWork_13.Models
{
    public class BaseRepository
    {
        static public object loker = new object();
        private Bank<Individual> individualList;
        private Bank<Business> businessList;
        private Bank<VipClient> vipClientsList;

        
        public Bank<Business> BusinessList { get => businessList; set => businessList = value; }
        public Bank<Individual> IndividualList { get => individualList; set => individualList = value; }
        public Bank<VipClient> VipClientsList { get => vipClientsList; set => vipClientsList = value; }

        public BaseRepository()
        {
            this.IndividualList = new Bank<Individual>();
            this.BusinessList = new Bank<Business>();
            this.VipClientsList = new Bank<VipClient>();
        }

        public BaseRepository(Bank<Individual> individuals,
            Bank<Business> businesses,
            Bank<VipClient> vipClients)
        {
            this.IndividualList = individuals;
            this.BusinessList = businesses;
            this.VipClientsList = vipClients;
        }

        public BaseRepository(BaseRepository saveBase)
        {
            individualList = saveBase.IndividualList;
            businessList = saveBase.BusinessList;
            vipClientsList = saveBase.VipClientsList;
        }

        public void FiilRepo()
        {
            try
            {
                //var individTask = Task.Factory.StartNew(()=>fillIndividual()) ; individTask.Start();
                //var vipTask = new Task(fillVip); vipTask.Start();
                //var businessTask = new Task(fillBusiness); businessTask.Start();
                Client.NewId();
                
                //Parallel.Invoke(fillIndividual, fillBusiness, fillVip);
                fillIndividual();
                fillBusiness();
                fillVip();
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
           
        }

        private void fillIndividual()
        {
            lock (loker)
            {
                for (int i = 0; i < 2_000_000; i++)
                {
                    
                    individualList.AddClient(new Individual($"IndividualClient - {i}", "Evergreen 123", "8-800-555-35-35"));
                }
            }
        }
        private void fillVip()
        {
            lock (loker)
            {
                for (int i = 0; i < 2_000_000; i++)
                {
                    vipClientsList.AddClient(new VipClient($"VipClient - {i}", "Evergreen 123", "8-800-555-35-35"));
                }
            }
        }

        private void fillBusiness()
        {
            lock (loker)
            {
                for (int i = 0; i < 2_000_000; i++)
                {
                    businessList.AddClient(new Business($"BusinessClient - {i}", "Evergreen 123", "8-800-555-35-35", "Homer", "WTF"));
                }
            }
        }
    }
}
