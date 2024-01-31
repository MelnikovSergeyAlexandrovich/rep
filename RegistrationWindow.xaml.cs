using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using AssetsIS.AssetsDataDataSetTableAdapters;

namespace AssetsIS
{
    public partial class RegistrationWindow : Window
    {
        RolesTableAdapter roles = new RolesTableAdapter();
        UsersTableAdapter users = new UsersTableAdapter();
        public List<RegistrationLog> logs = new List<RegistrationLog>();   
        public RegistrationWindow()
        {
            InitializeComponent();
            RoleIDComboBox.ItemsSource = roles.GetData();
            RoleIDComboBox.SelectedValuePath = "ID";
            RoleIDComboBox.DisplayMemberPath = "name";
            logs = JSONConverter.Deserialize<List<RegistrationLog>>("registrlogs.json");
        }
        

        private void RegistrationWindowButton_Click(object sender, RoutedEventArgs e)
        {
            const string phoneNumberMask = @"^(7)?[-\s]?\(?\d{3}\)?[-\s]?\d{3}[-\s]?\d{2}[-\s]?\d{2}$";
            Regex loginRegex = new Regex("^[a-zA-Z0-9]{5,20}$");
            Regex passwordRegex = new Regex("^[a-zA-Z0-9:?!@#$%^&*_+=/.,\\|]{5,20}$");
            Regex phoneNumberRegex = new Regex(phoneNumberMask);
            if ((LoginTextBox.Text == "") && (PasswordTextBox.Password == "") && (PhoneNumberTextBox.Text == "") && (RoleIDComboBox.SelectedItem != null))
            {
                MessageBox.Show("Все данные должны быть введены!");
            }
            else if ((loginRegex.IsMatch(LoginTextBox.Text)) && (passwordRegex.IsMatch(PasswordTextBox.Password)) && (phoneNumberRegex.IsMatch(PhoneNumberTextBox.Text) && (RoleIDComboBox.SelectedItem != null)))
            {
                int id = Convert.ToInt32(RoleIDComboBox.SelectedValue);
                string filteredPhoneNumber = PhoneNumberTextBox.Text.Replace("-", "").Replace(" ", "");
                users.InsertQuery(id, LoginTextBox.Text, CryptoGraphy.CreateSHA256(PasswordTextBox.Password), filteredPhoneNumber);
                MessageBox.Show("Вы успешно зарегестрировались!");
                RegistrationLog log = new RegistrationLog("Регистрация вручную", DateTime.Now, id);
                logs.Add(log);
                JSONConverter.Serialize(logs, "registrlogs.json");
                MainWindow mainWindow = new MainWindow();   
                mainWindow.Show();
                Close();
            }
            else if (!loginRegex.IsMatch(LoginTextBox.Text))
            {
                MessageBox.Show("Логин был введён неверно! Он должен содержать только латиницу, цифры, а также иметь длину от 5 до 20 символов!");
            }
            else if (!passwordRegex.IsMatch(PasswordTextBox.Password))
            {
                MessageBox.Show("Пароль был введён неверно! Он должен содержать только латиницу, цифры, специальные символы, а также иметь длину от 5 до 20 символов!");
            }
            else if (!phoneNumberRegex.IsMatch(PhoneNumberTextBox.Text))
            {
                MessageBox.Show("Номер телефона был введён неверно! Он должен иметь форму '7-984-560-20-12' или '79845602012'");
            }
            else if ((!loginRegex.IsMatch(LoginTextBox.Text)) && (!passwordRegex.IsMatch(PasswordTextBox.Password)) && (!phoneNumberRegex.IsMatch(PhoneNumberTextBox.Text)))
            {
                MessageBox.Show("Данные были введенны неверно!");
            }
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
