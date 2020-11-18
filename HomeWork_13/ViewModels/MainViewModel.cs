using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HomeWork_13.Models;
using Microsoft.Win32;
using CustomExceptions;
using BankClassLibrary;

namespace HomeWork_13.ViewModels
{
    public class MainViewModel
    {
        private BaseRepository repository;
        
        public BaseRepository Repository { get => repository; }

        public MainViewModel()
        {
            repository = new BaseRepository();
        }

        public void Load(string path)
        {
            var ser = new Serializer();
                if (ser.Load(path))
                {
                    repository = ser.Repo;
                    if (repository.IndividualList == null)
                        repository.IndividualList = new Bank<Individual>();
                    if (repository.BusinessList == null)
                        repository.BusinessList = new Bank<Business>();
                    if (repository.VipClientsList == null)
                        repository.VipClientsList = new Bank<VipClient>();
                }
                else
                    throw new LoadException("Данный файл не подходит для загрузки");
            }

        public void Save(string path)
        {
            Serializer ser = new Serializer(
               new BaseRepository(repository));
            
            Task.Factory.StartNew(()=>ser.Save(path));

        }

        public void CreateManyCLientsRepo()
        {

             Task.Factory.StartNew(()=>repository.FiilRepo());
             MessageBox.Show("Идет генерация");
        }
    }
}
