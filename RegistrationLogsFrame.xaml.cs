using System.Collections.Generic;
using System.Windows.Controls;


namespace AssetsIS
{
    public partial class RegistrationLogsFrame : Page
    {
        public RegistrationLogsFrame()
        {
            InitializeComponent();
            RegistrationLogsDataGrid.IsReadOnly = true;
            List<RegistrationLog> list = new List<RegistrationLog>();
            list = JSONConverter.Deserialize<List<RegistrationLog>>("registrlogs.json");
            RegistrationLogsDataGrid.ItemsSource = list;
        }
    }
}
