using System.Windows;


namespace AssetsIS
{
    public partial class DataWindow : Window
    {
        public DataWindow()
        {
            InitializeComponent();
        }

        private void RegistrationWindowButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu(); 
            mainMenu.Show();
            Close();
        }

        private void RolesFrameButton_Click(object sender, RoutedEventArgs e)
        {
            AdminDataFrame.Content = new RolesFrame(); 
        }

        private void UsersFrameButton_Click(object sender, RoutedEventArgs e)
        {
            AdminDataFrame.Content = new UsersFrame();

        }

        private void InventoryFrameButton_Click(object sender, RoutedEventArgs e)
        {
            AdminDataFrame.Content = new InventoryFrame();    
        }

        private void FacilitiesFrameButton_Click(object sender, RoutedEventArgs e)
        {
            AdminDataFrame.Content = new FacilitiesFrame();
        }

        private void AssetsFrameButton_Click(object sender, RoutedEventArgs e)
        {
            AdminDataFrame.Content = new AssetsFrame();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            Close();
        }
    }
}
