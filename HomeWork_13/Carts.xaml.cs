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
using System.Windows.Shapes;
using HomeWork_13.Models;


namespace HomeWork_13
{
    /// <summary>
    /// Логика взаимодействия для Carts.xaml
    /// </summary>
    public partial class Carts : Window
    {
        private Client currentClient; // Текущий клиент
        
        public Carts()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Перезагрузка конструктора
        /// </summary>
        /// <param name="client">Экземпляр клиента</param>
        public Carts(Client client) :base()
        {
            InitializeComponent();
           
            CartListGrid.ItemsSource= client.Carts;
            currentClient = client;

            
        }
        

        /// <summary>
        /// Действие по нажатию кнопки открытия кредитного счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenCredit_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(CreditLimitBox.Text))
            {
                double limit;
                if (Double.TryParse(CreditLimitBox.Text, out limit) && limit>0)
                    if (currentClient.CheckAndOpenAccount(Account.AccountTypes.Credit, 0, limit))
                    {
                        OpenCreditPanel.Visibility = Visibility.Visible;
                        SaveAccPanel.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        OpenCreditPanel.Visibility = Visibility.Collapsed;
                        MessageBox.Show("Такой счет уже имеется");
                    }
                else
                    MessageBox.Show("Не подходящее значение");
            }
            else
                MessageBox.Show("Введите значение кредитного лимита");
        }


        private void CartListGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (CartListGrid.Items.Count != 0 && CartListGrid.SelectedItem as Account != null )
                {
                    
                    
                    
                    if (CartListGrid.SelectedItem is SaveAccount)
                    {
                        var currAccount = (SaveAccount)CartListGrid.SelectedItem;
                        ListOfLogTransaction.ItemsSource = currAccount.LogTransaction;
                        
                        SaveAccPanel.Visibility = Visibility.Visible;
                        GridCreditAccPanel.Visibility = Visibility.Collapsed;
                        if ((currAccount as SaveAccount).CompleteInvestmentDate == DateTime.MinValue)
                        {
                            InvestmentCompleteDateBox.Text = "Вклад еще не сделан";
                            InvestmentStartDateBox.Text = "Вклад еще не сделан";
                        }
                        else
                        {
                            InvestmentCompleteDateBox.Text = $"{(currAccount as SaveAccount).CompleteInvestmentDate}";
                            InvestmentStartDateBox.Text = $"{(currAccount as SaveAccount).StartInvestmentDate}";
                        }
                    }
                    else
                    {
                        var currAccount = (CreditAccount)CartListGrid.SelectedItem;
                        ListOfLogTransaction.ItemsSource = currAccount.LogTransaction;
                        SaveAccPanel.Visibility = Visibility.Collapsed;
                        GridCreditAccPanel.Visibility = Visibility.Visible;
                        CreditBalanceBlock.Text = $"{(currAccount as CreditAccount).CreditBalance}";
                        CurrentLimitBlock.Text = $"{(currAccount as CreditAccount).Limit}";
                    }
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Что то пошло не так ({ex.Message})");
            }
        }

        /// <summary>
        /// Обработка нажатия по кнопке внесения средств
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepositButton_Click(object sender, RoutedEventArgs e)
        {
            if (CartListGrid.SelectedItem is null)
                MessageBox.Show("Выберите счет");
            else
            {
                if (!String.IsNullOrWhiteSpace(DepositBox.Text)) //Проверка на пустой TextBox
                {
                    var currentAc = (Account)CartListGrid.SelectedItem;
                    double amount;
                    if (double.TryParse(DepositBox.Text, out amount) && amount>0)
                        currentAc.Deposit(amount);
                    else
                        MessageBox.Show("Введенное значение не верное");
                }
                else
                    MessageBox.Show("Введенное значение не верное");
            }
        }
        /// <summary>
        /// Обработка кнопки с началом вклада на счет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartInvestmentButton_Click(object sender, RoutedEventArgs e)
        {
            var currentAc = (SaveAccount)CartListGrid.SelectedItem;
            bool flag;
            if ((WithCapitalizationCheckBox.IsChecked == true) || (WithOutCapitalizationCheckBox.IsChecked == true))
            {
                if ((bool)WithCapitalizationCheckBox.IsChecked)
                    flag = true;
                else
                    flag = false;

                if (currentAc != null)
                {
                    double amount;
                    byte month;
                    if (Double.TryParse(InvestmentBox.Text, out amount) && 
                        !String.IsNullOrWhiteSpace(InvestmentBox.Text) && 
                        !String.IsNullOrWhiteSpace(InvestmentMountBox.Text) &&
                        Byte.TryParse(InvestmentMountBox.Text, out month)) //проверка на ввод значения в TextBox 
                        if(amount>0 && month>0)
                            if (currentAc.StartInvestment(amount, month, flag))
                            {
                                InvestmentStartDateBox.Text = $"{(currentAc as SaveAccount).StartInvestmentDate}";
                                InvestmentCompleteDateBox.Text = $"{(currentAc as SaveAccount).CompleteInvestmentDate}";
                                MessageBox.Show("Вы сделали вклад!");

                            }
                            else
                                MessageBox.Show("На счету не достаточно средств, либо у вас уже есть активный вклад");
                         else MessageBox.Show("Вводимые данные должны быть больше 0");
                    else
                        MessageBox.Show("Некоректный ввод суммы для вклада");


                }
                else
                    MessageBox.Show("Выберите счет");
            }
            else
                MessageBox.Show("Выберите тип капитализации");

        }
        /// <summary>
        /// Обработка кнопки по завершению вклада на аккаунте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompleteInvestmentButton_Click(object sender, RoutedEventArgs e)
        {

            if (!(CartListGrid.SelectedItem as SaveAccount).CheckInvestment()) MessageBox.Show("У вас еще нет активных вкладов");
            
            //TimeSpan timer = currentAc.CompleteInvestmentDate - currentAc.StartInvestmentDate;
            //currentAc.CheckInvestment();

                
        }

        private void AddCreditButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(AddCreditBox.Text)) //Проверка на пустой TextBox
            {
                var currentAc = (CreditAccount)CartListGrid.SelectedItem;
                double amount;
                if (double.TryParse(AddCreditBox.Text, out amount) && amount>0)
                    if (currentAc.GetCredit(amount))
                    {
                        CreditBalanceBlock.Text =$"{currentAc.CreditBalance}";
                    }
                    else
                        MessageBox.Show("Что то пошло не так");
                else
                    MessageBox.Show("Введенное значение не верное");
            }
            else
                MessageBox.Show("Введенное значение не верное");
        }

        private void CloseCreditButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(CloseCreditBox.Text)) //Проверка на пустой TextBox
            {
                var currentAc = (CreditAccount)CartListGrid.SelectedItem;
                double amount;
                if (double.TryParse(CloseCreditBox.Text, out amount) && amount>0)
                {
                    currentAc.CloseCredit(amount);
                    CreditBalanceBlock.Text = $"{currentAc.CreditBalance}";
                }
                else
                    MessageBox.Show("Введенное значение не верное");
            }
            else
                MessageBox.Show("Введенное значение не верное");
        }

        private void WithCapitalizationCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            WithOutCapitalizationCheckBox.IsChecked = !WithCapitalizationCheckBox.IsChecked;
        }

        private void WithOutCapitalizationCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            WithCapitalizationCheckBox.IsChecked = !WithOutCapitalizationCheckBox.IsChecked;
        }

        private void InvestmentMenu_Click(object sender, RoutedEventArgs e)
        {
            if (CartListGrid.Items.Count != 0 && CartListGrid.SelectedItem as Account != null)
            {
                if (CartListGrid.SelectedItem is SaveAccount)
                {
                    var currAccount = (SaveAccount)CartListGrid.SelectedItem;
                    SaveAccPanel.Visibility = Visibility.Visible;
                    GridCreditAccPanel.Visibility = Visibility.Collapsed;
                    if ((currAccount as SaveAccount).CompleteInvestmentDate == DateTime.MinValue)
                    {
                        InvestmentCompleteDateBox.Text = "Вклад еще не сделан";
                        InvestmentStartDateBox.Text = "Вклад еще не сделан";
                    }
                    else
                    {
                        InvestmentCompleteDateBox.Text = $"{(currAccount as SaveAccount).CompleteInvestmentDate}";
                        InvestmentStartDateBox.Text = $"{(currAccount as SaveAccount).StartInvestmentDate}";
                    }
                }
            }
        }

        private void OpenSaveMenu_Click(object sender, RoutedEventArgs e)
        {
            if (currentClient.CheckAndOpenAccount(Account.AccountTypes.Debit, 0, 0))
            {
               
                OpenCreditPanel.Visibility = Visibility.Collapsed;
                SaveAccPanel.Visibility = Visibility.Visible; 
            }
            else
                MessageBox.Show("Такой счет уже имеется");
        }

        private void OpenCreditMenu_Click(object sender, RoutedEventArgs e)
        {
            OpenCreditPanel.Visibility = Visibility.Visible;
            SaveAccPanel.Visibility = Visibility.Collapsed;
        }

        private void CartListGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string header = e.Column.Header.ToString();
            if (header == "LogTransaction")
                e.Column.Visibility = Visibility.Collapsed;
        }
    }
}
