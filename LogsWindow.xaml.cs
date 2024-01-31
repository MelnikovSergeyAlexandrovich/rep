using System.Windows;

namespace AssetsIS
{
    public partial class LogsWindow : Window
    {
        public LogsWindow()
        {
            InitializeComponent();
        }

        private void ReturnLogsButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            Close();
        }

        private void RegistrationLogsButton_Click(object sender, RoutedEventArgs e)
        {
            LogsFrame.Content = new RegistrationLogsFrame();
        }

        private void RequestsLogsButton_Click(object sender, RoutedEventArgs e)
        {
            LogsFrame.Content = new RequestsLogsFrame();
        }
    }
}
