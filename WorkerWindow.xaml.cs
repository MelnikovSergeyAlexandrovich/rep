
using System.Windows;


namespace AssetsIS
{

    public partial class WorkerWindow : Window
    {
        public WorkerWindow()
        {
            InitializeComponent();
        }

        private void DataWindowButton_Click(object sender, RoutedEventArgs e)
        {
            InventoryWorkerWindow workerWindow = new InventoryWorkerWindow();   
            workerWindow.Show();    
        }

        private void RequestsWindowButton_Click(object sender, RoutedEventArgs e)
        {
            RequestsWorkerWindow workerWindow = new RequestsWorkerWindow();
            workerWindow.Show();
        }

        private void LogOutWindowButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
