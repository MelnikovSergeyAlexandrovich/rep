using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using AssetsIS.AssetsDataDataSetTableAdapters;

namespace AssetsIS
{
    public partial class CreateRequest : Window
    {
        InventoryTableAdapter inventory = new InventoryTableAdapter();
        const string nameMask = @"[0-9а-яА-Я\s]{5,50}";
        readonly Regex nameRegex = new Regex(nameMask);
        List<Request> requests = new List<Request>();
        List<RequestLog> logs = new List<RequestLog>();
        public CreateRequest()
        {
            InitializeComponent();
            InventoryComboBox.ItemsSource = inventory.GetData();
            InventoryComboBox.SelectedValuePath = "ID";
            InventoryComboBox.DisplayMemberPath = "name";
            requests = JSONConverter.Deserialize<List<Request>>("requests.json");
            logs = JSONConverter.Deserialize<List<RequestLog>>("reqslogs.json");
        }

        private void GetBackButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerMenu managerMenu = new ManagerMenu();
            managerMenu.Show();
            Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (InventoryComboBox.SelectedItem != null && NameTextBox.Text != "" && DescriptionTextBox.Text != "")
            {
                if (InventoryComboBox.SelectedItem != null)
                {
                    if(nameRegex.IsMatch(NameTextBox.Text))
                    {
                        if (DescriptionTextBox.Text.Length > 400) 
                        {
                            MessageBox.Show("Длина описания не может быть больше 400 символов!");
                        }
                        else
                        {
                            string uuid = Convert.ToString(Guid.NewGuid());
                            Request request = new Request(uuid, NameTextBox.Text, DescriptionTextBox.Text, Convert.ToInt32(InventoryComboBox.SelectedValue), false);
                            RequestLog log = new RequestLog(DateTime.Now, NameTextBox.Text, uuid, false);
                            requests.Add(request);
                            logs.Add(log);
                            JSONConverter.Serialize(requests, "requests.json");
                            JSONConverter.Serialize(logs, "reqslogs.json");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Название должно состоять только из пробелов и кириллицы,  а также иметь длину в 5 - 50 символов!");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите предмет инвентаря!");
                }

            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }
        }

        private void CheckRequestsButton_Click(object sender, RoutedEventArgs e)
        {
            RequestDataWindow requestDataWindow = new RequestDataWindow();
            requestDataWindow.Show();
        }
    }
}
