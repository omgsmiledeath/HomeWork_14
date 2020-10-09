using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HomeWork_13.Models;
using Microsoft.Win32;
using HomeWork_13.ViewModels;
using BankClassLibrary;
namespace HomeWork_13
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mvm;
        
        public MainWindow()
        {
            InitializeComponent();
            mvm = new MainViewModel();
        }

        /// <summary>
        /// Обработка загрузки главного окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }

        /// <summary>
        /// Обработка нажатия кнопки открытия страницы с обычными клиентами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenIndividualClientsPage(object sender, RoutedEventArgs e)
        {
            
            // MainFrame.Content = new Clients(individualBank.ClientList);
            
            MainFrame.Content = new Clients()
            {
                DataContext = new IndividualViewModel(mvm.
                Repository.
                IndividualList.
                ClientList)
            };
        }
        /// <summary>
        /// Обработка нажатия кнопки открытия страницы с бизнес клиентами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenBusinessClientsPage(object sender, RoutedEventArgs e)
        {

            MainFrame.Content = new Clients()
            {
                DataContext = new BusinessViewModel(mvm.
                Repository.
                BusinessList.
                ClientList)
            };
        }
        /// <summary>
        /// Обработка нажатия кнопки открытия страницы с вип клиентами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenVipClientsPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Clients()
            {
                DataContext = new VipViewModel(mvm.
                Repository.
                VipClientsList.
                ClientList)
            };
        }

        private void OpenAllAccountsPage(object sender, RoutedEventArgs e)
        {
            
            MainFrame.Content = new AllAccounts(
                new ObservableCollection<Account>(
                    mvm.Repository.IndividualList.ClientList
                    .SelectMany(t => t.Carts)
                    .Concat(mvm.Repository.VipClientsList.ClientList.SelectMany(t => t.Carts))
                    .Concat(mvm.Repository.BusinessList.ClientList.SelectMany(t => t.Carts))
                    )
            );
        }

        private void OpenBaseMenu_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            //Nullable<bool> result = ofd.ShowDialog();
            //string path = ofd.FileName;
            //if (path != string.Empty)
            //{
            //    mvm.Load(path);
            //    MainFrame.Content = null;
            //}
            mvm.CreateManyCLientsRepo();   
        }

        private void SaveBaseMenu_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            Nullable<bool> result = sfd.ShowDialog();
            string path = sfd.FileName;
            if (path != string.Empty) mvm.Save(path);
        }

        private void ExitMenu_Clikc(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    
    }
}
