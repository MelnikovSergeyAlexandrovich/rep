using System.Collections.Generic;
using System.Windows.Controls;


namespace AssetsIS
{

    public partial class RequestsLogsFrame : Page
    {
        List<RequestLog> logs = new List<RequestLog>();
        public RequestsLogsFrame()
        {
            InitializeComponent();
            RequestsLogsDataGrid.IsReadOnly = true;
            logs = JSONConverter.Deserialize<List<RequestLog>>("reqslogs.json");
            RequestsLogsDataGrid.ItemsSource = logs;
        }
    }
}
