using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using AssetsIS.AssetsDataDataSetTableAdapters;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace AssetsIS
{
    public partial class UsersFrame : System.Windows.Controls.Page
    {
        UsersTableAdapter users = new UsersTableAdapter();
        RolesTableAdapter roles = new RolesTableAdapter();
        const string phoneNumberMask = @"^(7)?[-\s]?\(?\d{3}\)?[-\s]?\d{3}[-\s]?\d{2}[-\s]?\d{2}$";
        readonly Regex loginRegex = new Regex("^[a-zA-Z0-9]{5,20}$");
        readonly Regex passwordRegex = new Regex("^[a-zA-Z0-9:?!@#$%^&*_+=/.,\\|]{5,20}$");
        readonly Regex phoneNumberRegex = new Regex(phoneNumberMask);
        public List<RegistrationLog> logs = new List<RegistrationLog>();
        
        public UsersFrame()
        {
            InitializeComponent();
            UsersDataGrid.ItemsSource = users.GetData();
            RoleIDComboBox.ItemsSource = roles.GetData();
            RoleIDComboBox.SelectedValuePath = "ID";
            RoleIDComboBox.DisplayMemberPath = "name";
            UsersDataGrid.IsReadOnly = true;
            logs = JSONConverter.Deserialize<List<RegistrationLog>>("registrlogs.json");

        }

        private void CreateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
           
            if ((LoginTextBox.Text == "") && (PasswordTextBox.Password == "") && (PhoneNumberTextBox.Text == "") && (RoleIDComboBox.SelectedItem != null))
            {
                MessageBox.Show("Все данные должны быть введены!");
            }
            else if ((loginRegex.IsMatch(LoginTextBox.Text)) && (passwordRegex.IsMatch(PasswordTextBox.Password)) && (phoneNumberRegex.IsMatch(PhoneNumberTextBox.Text) && (RoleIDComboBox.SelectedItem != null)))
            {
                int id = Convert.ToInt32(RoleIDComboBox.SelectedValue);
                string filteredPhoneNumber = PhoneNumberTextBox.Text.Replace("-", "").Replace(" ", "");
                users.InsertQuery(id, LoginTextBox.Text, CryptoGraphy.CreateSHA256(PasswordTextBox.Password), filteredPhoneNumber);
                UsersDataGrid.ItemsSource = users.GetData();
                RegistrationLog log = new RegistrationLog("Регистрация администратором", DateTime.Now, id);
                logs.Add(log);
                JSONConverter.Serialize(logs, "registrlogs.json");
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
            else if (RoleIDComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите роль!");
            }
            else if ((!loginRegex.IsMatch(LoginTextBox.Text)) && (!passwordRegex.IsMatch(PasswordTextBox.Password)) && (!phoneNumberRegex.IsMatch(PhoneNumberTextBox.Text)))
            {
                MessageBox.Show("Данные были введенны неверно!");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem != null)
            {
                var id = (UsersDataGrid.SelectedItem as DataRowView).Row[0];
                users.DeleteQuery(Convert.ToInt32(id));
                UsersDataGrid.ItemsSource = users.GetData();
                RegistrationLog log = new RegistrationLog("Удаление администратором", DateTime.Now, Convert.ToInt32(id));
                logs.Add(log);
                JSONConverter.Serialize(logs, "registrlogs.json");
            }
            else
            {
                MessageBox.Show("Выберите пользователя из таблицы!");
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if ((LoginTextBox.Text == "") && (PasswordTextBox.Password == "") && (PhoneNumberTextBox.Text == "") && (RoleIDComboBox.SelectedItem != null))
            {
                MessageBox.Show("Все данные должны быть введены!");
            }
            else if (UsersDataGrid.SelectedItem != null)
            {
                object ID = (UsersDataGrid.SelectedItem as DataRowView).Row[0]; 
                if (RoleIDComboBox.SelectedItem != null)
                {
                    int roleID = Convert.ToInt32(RoleIDComboBox.SelectedValue);
                    if (loginRegex.IsMatch(LoginTextBox.Text))
                    {
                        if(passwordRegex.IsMatch(PasswordTextBox.Password))
                        {
                            if (phoneNumberRegex.IsMatch(PhoneNumberTextBox.Text))
                            {
                                users.UpdateQuery(roleID,LoginTextBox.Text,CryptoGraphy.CreateSHA256(PasswordTextBox.Password), PhoneNumberTextBox.Text, Convert.ToInt32(ID));
                                UsersDataGrid.ItemsSource = users.GetData();
                                RegistrationLog log = new RegistrationLog("Изменение администратором", DateTime.Now, Convert.ToInt32(ID));
                                logs.Add(log);
                                JSONConverter.Serialize(logs, "registrlogs.json");
                            }
                            else
                            {
                                MessageBox.Show("Номер телефона был введён неверно! Он должен иметь форму '7-984-560-20-12' или '79845602012'");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пароль был введён неверно! Он должен содержать только латиницу, цифры, специальные символы, а также иметь длину от 5 до 20 символов!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Логин был введён неверно! Он должен содержать только латиницу, цифры, а также иметь длину от 5 до 20 символов!");
                    }

                }
                else
                {
                    MessageBox.Show("Выберите роль!");
                }
            }
        }

        private void UsersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem != null)
            {
                LoginTextBox.Text = Convert.ToString((UsersDataGrid.SelectedItem as DataRowView).Row[2]);
                PhoneNumberTextBox.Text = Convert.ToString((UsersDataGrid.SelectedItem as DataRowView).Row[4]);
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            DataManipulation.ExportData("users", "Users");
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CSV Files (*.csv)|*.csv";
            if (dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;
                FileInfo fileInfo = new FileInfo(path);
                long fileSizeInBytes = fileInfo.Length;
                if (fileSizeInBytes / (1024.0 * 1024.0) > 2)
                {
                    MessageBox.Show("Вес импортируемого файла не должен превышать 2-х Мегабайт.");
                }
                else
                {
                    DataTable csvData = ImportData.GetDataTabletFromCSVFile(path);
                    ImportData.InsertDataIntoSQLServerUsingSQLBulkCopy(csvData, "Users");
                    UsersDataGrid.ItemsSource = users.GetData();
                }
            }
        }
    }
}
