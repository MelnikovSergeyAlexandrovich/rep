using System;
using System.Text.RegularExpressions;
using System.Windows;
using AssetsIS.AssetsDataDataSetTableAdapters;

namespace AssetsIS
{
    public partial class MainWindow : Window
    {
        UsersTableAdapter users = new UsersTableAdapter();
        public MainWindow()   
        {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            Regex loginRegex = new Regex("^[a-zA-Z0-9]{5,20}$");
            Regex passwordRegex = new Regex("^[a-zA-Z0-9:?!@#$%^&*_+=/.,\\|]{5,20}$");
            if ((LoginTextBox.Text == "") && (PasswordTextBox.Password == ""))
            {
                MessageBox.Show("Все данные должны быть введены!");
            }
            else if ((loginRegex.IsMatch(LoginTextBox.Text)) && (passwordRegex.IsMatch(PasswordTextBox.Password)))
            {
                bool IsAutorized = false;
                var usersData = users.GetData().Rows;
                int i = 0;
                for (;i < usersData.Count; i++)
                {
                    if ((usersData[i][2].ToString() == LoginTextBox.Text) && (usersData[i][3].ToString() == CryptoGraphy.CreateSHA256(PasswordTextBox.Password)))
                    {

                        IsAutorized = true;
                        break;
                    }
                }
                if (IsAutorized)
                {

                    if (Convert.ToInt32(usersData[i][1]) == 6) // 6 - Роль "Администратор" 
                    {
                        MainMenu mainmenu = new MainMenu();
                        mainmenu.Show();
                        Close();
                    }
                    else if (Convert.ToInt32(usersData[i][1]) == 1) // 1 - Роль "Менеджер"
                    {
                        ManagerMenu managermenu = new ManagerMenu();    
                        managermenu.Show();
                        Close();
                    }
                    else if (Convert.ToInt32(usersData[i][1]) == 4) // 4 - Роль "Техник"
                    {
                        WorkerWindow workerWindow = new WorkerWindow();
                        workerWindow.Show();
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Данные были введены неверно!");
                }
            }
            else if (!loginRegex.IsMatch(LoginTextBox.Text))
            {
                MessageBox.Show("Логин был введён неверно!");
            }
            else if (!passwordRegex.IsMatch(PasswordTextBox.Password))
            {
                MessageBox.Show("Пароль был введён неверно!");
            }
            else if ((!loginRegex.IsMatch(LoginTextBox.Text)) && (!passwordRegex.IsMatch(PasswordTextBox.Password)))
            {
                MessageBox.Show("Данные были введенны неверно!");
            }
        }

        private void RegistrationWindowButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow window = new RegistrationWindow();
            window.Show();
            Close();
        }
    }
}
