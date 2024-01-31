using System.Collections.Generic;
using System.Windows;

namespace AssetsIS
{
    public partial class RequestDataWindow : Window
    {
        
        public RequestDataWindow()
        {
            InitializeComponent();
            RequestsDataGrid.IsReadOnly = true;
            List<Request> requests = new List<Request>();
            requests = JSONConverter.Deserialize<List<Request>>("requests.json");
            RequestsDataGrid.ItemsSource = requests;
        }
    }
}
