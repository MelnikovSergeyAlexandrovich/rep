
using System.Windows;


namespace AssetsIS
{
    public partial class DataForManagerWindow : Window
    {
        public DataForManagerWindow()
        {
            InitializeComponent();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerMenu managerMenu = new ManagerMenu();    
            managerMenu.Show();
            Close();
        }

        private void InventoryButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerDataFrame.Content = new InventoryManagerFrame();
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerDataFrame.Content = new UsersManagerFrame();
        }
    }
}
