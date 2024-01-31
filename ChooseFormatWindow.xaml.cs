
using System.Windows;


namespace AssetsIS
{
    public partial class ChooseFormatWindow : Window
    {
        public bool isCSV;
        public ChooseFormatWindow()
        {
            InitializeComponent();
        }

        private void xlsxChoosenRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            isCSV = false;
            DialogResult = true;
        }

        private void csvChoosenRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            isCSV = true;
            DialogResult = true;
        }
    }
}
