using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_13.Models
{
    public class BaseRepository
    {
        private Bank<Individual> individualList;
        private Bank<Business> businessList;
        private Bank<VipClient> vipClientsList;

        public Bank<Business> BusinessList { get => businessList; set => businessList = value; }
        public Bank<Individual> IndividualList { get => individualList; set => individualList = value; }
        public Bank<VipClient> VipClientsList { get => vipClientsList; set => vipClientsList = value; }

        public BaseRepository()
        {

        }

        public BaseRepository(Bank<Individual> individuals,
            Bank<Business> businesses,
            Bank<VipClient> vipClients)
        {
            this.IndividualList = individuals;
            this.BusinessList = businesses;
            this.VipClientsList = vipClients;
        }

    }
}
