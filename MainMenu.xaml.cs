
using System.Windows;

namespace AssetsIS
{
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void LogOutWindowButton_Click(object sender, RoutedEventArgs e)
        {
           MainWindow mainwindow = new MainWindow();
           mainwindow.Show();
           Close();
        }

        private void LogsWindowButton_Click(object sender, RoutedEventArgs e)
        {
            LogsWindow logswindow = new LogsWindow();
            logswindow.Show();
            Close();
        }

        private void DataWindowButton_Click(object sender, RoutedEventArgs e)
        {
            DataWindow dataWindow = new DataWindow();
            dataWindow.Show();
            Close();
        }
    }
}
