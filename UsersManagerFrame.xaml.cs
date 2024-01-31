using System.Windows.Controls;
using AssetsIS.AssetsDataDataSetTableAdapters;

namespace AssetsIS
{
    public partial class UsersManagerFrame : Page
    {
        UsersTableAdapter users = new UsersTableAdapter();
        public UsersManagerFrame()
        {
            InitializeComponent();
            UsersDataGrid.IsReadOnly = true; 
            UsersDataGrid.ItemsSource = users.GetData();
        }

        private void ExportButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DataManipulation.ExportData("users", "Users");
        }
    }
}
