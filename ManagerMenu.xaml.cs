using System.Windows;


namespace AssetsIS
{
    public partial class ManagerMenu : Window
    {
        public ManagerMenu()
        {
            InitializeComponent();
        }

        private void LogOutWindowButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void DataWindowButton_Click(object sender, RoutedEventArgs e)
        {
            DataForManagerWindow dataForManagerWindow = new DataForManagerWindow();
            dataForManagerWindow.Show();
            Close();
        }

        private void RequestsWindowButton_Click(object sender, RoutedEventArgs e)
        {
            CreateRequest createRequest = new CreateRequest();  
            createRequest.Show();
            Close();
        }
    }
}
