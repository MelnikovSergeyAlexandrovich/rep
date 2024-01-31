using System.Windows;
using AssetsIS.AssetsDataDataSetTableAdapters;

namespace AssetsIS
{
    public partial class InventoryWorkerWindow : Window
    {
        InventoryTableAdapter inventory = new InventoryTableAdapter();
        public InventoryWorkerWindow()
        {
            InitializeComponent();
            InventoryDataGrid.IsReadOnly = true;
            InventoryDataGrid.ItemsSource = inventory.GetData();
        }
    }
}
