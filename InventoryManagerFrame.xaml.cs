using System.Windows.Controls;
using AssetsIS.AssetsDataDataSetTableAdapters;

namespace AssetsIS
{
    public partial class InventoryManagerFrame : Page
    {
        InventoryTableAdapter inventory = new InventoryTableAdapter();
        public InventoryManagerFrame()
        {
            InitializeComponent();
            InventoryDataGrid.IsReadOnly = true;
            InventoryDataGrid.ItemsSource = inventory.GetData();
        }

        private void ExportButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DataManipulation.ExportData("inventory", "Inventory");
        }
    }
}
