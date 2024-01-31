using System;
using System.Collections.Generic;
using System.Windows;
using AssetsIS.AssetsDataDataSetTableAdapters;

namespace AssetsIS
{
    public partial class RequestsWorkerWindow : Window
    {
        List<Request> requests = new List<Request>();
        List<RequestLog> logs = new List<RequestLog>();
        public RequestsWorkerWindow()
        {
            InitializeComponent();
            requests = JSONConverter.Deserialize<List<Request>>("requests.json");
            logs = JSONConverter.Deserialize<List<RequestLog>>("reqslogs.json");
            RequestsDataGrid.IsReadOnly = true;
            RequestsDataGrid.ItemsSource = requests;
            List<Item> items = new List<Item>
            {
                new Item { Status = true, Name = "Эксплуатируется" },
                new Item { Status = false, Name = "Не Эксплуатируется" }
            };
            StatusComboBox.ItemsSource = items;
            StatusComboBox.SelectedValuePath = "Status";
            StatusComboBox.DisplayMemberPath = "Name";
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsDataGrid.SelectedItem != null)
            {
                int id = RequestsDataGrid.SelectedIndex;
                if (StatusComboBox.SelectedItem != null)
                {

                        for (int i = 0; i < requests.Count; i++)
                        {
                            if (i == id)
                            {
                                requests[i].status = Convert.ToBoolean(StatusComboBox.SelectedValue);
                                JSONConverter.Serialize(requests, "requests.json");
                                requests = JSONConverter.Deserialize<List<Request>>("requests.json");
                                RequestsDataGrid.ItemsSource = requests;
                                RequestLog log = new RequestLog(DateTime.Now, requests[i].name, requests[id].id, Convert.ToBoolean(StatusComboBox.SelectedValue));
                                logs.Add(log);
                                JSONConverter.Serialize(logs, "reqslogs.json");
                            }
                        }
                    
                }
                else
                {
                    MessageBox.Show("Выберите статус!");
                }
            }
            else
            {
                MessageBox.Show("Выберите запрос!");
            }
        }
    }
}
