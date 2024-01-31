
using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using AssetsIS.AssetsDataDataSetTableAdapters;
using Microsoft.Win32;

namespace AssetsIS
{
    public partial class FacilitiesFrame : Page
    {
        FacilitiesTableAdapter facilities = new FacilitiesTableAdapter();
        UsersTableAdapter users = new UsersTableAdapter();  
        InventoryTableAdapter inventory = new InventoryTableAdapter();
        const string spaceMask = @"[+]?\d*\.?\d+"; // Маска для чисел Double
        const string nameMask = @"[0-9а-яА-Я\s]{5,50}";
        readonly Regex nameRegex = new Regex(nameMask);
        readonly Regex spaceRegex = new Regex(spaceMask);
        public FacilitiesFrame()
        {
            InitializeComponent();
            FacilitiesDataGrid.IsReadOnly = true;
            FacilitiesDataGrid.ItemsSource = facilities.GetData();
            UserComboBox.ItemsSource = users.GetData();
            UserComboBox.SelectedValuePath = "ID";
            UserComboBox.DisplayMemberPath = "ID";
            InventoryComboBox.ItemsSource = inventory.GetData();
            InventoryComboBox.SelectedValuePath = "ID";
            InventoryComboBox.DisplayMemberPath = "name";
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == "" && SpaceTextBox.Text == "" && UserComboBox.SelectedItem == null && InventoryComboBox.SelectedItem == null)
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }
            else if (UserComboBox.SelectedItem != null)
            {
                if (InventoryComboBox.SelectedItem != null)
                {
                    if (nameRegex.IsMatch(NameTextBox.Text))
                    {
                        if (spaceRegex.IsMatch(SpaceTextBox.Text))
                        {
                            int inventoryID = Convert.ToInt32(InventoryComboBox.SelectedValue);
                            int userID = Convert.ToInt32(UserComboBox.SelectedValue);
                            facilities.InsertQuery(NameTextBox.Text, Convert.ToDouble(SpaceTextBox.Text), userID, inventoryID);
                            FacilitiesDataGrid.ItemsSource = facilities.GetData();
                        }
                        else
                        {
                            MessageBox.Show("Площадь должна состоять только из цифр и точек!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Название помещения должно содержать только кириллицу и иметь длину от 5 до 50 символов!");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите предмет инвентаря!");
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя!");
            }
        }

        private void FaciliesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FacilitiesDataGrid.SelectedItem != null)
            {
                NameTextBox.Text = Convert.ToString((FacilitiesDataGrid.SelectedItem as DataRowView).Row[1]);
                SpaceTextBox.Text = Convert.ToString((FacilitiesDataGrid.SelectedItem as DataRowView).Row[2]);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (FacilitiesDataGrid.SelectedItem != null)
            {
                var id = (FacilitiesDataGrid.SelectedItem as DataRowView).Row[0];
                facilities.DeleteQuery(Convert.ToInt32(id));
                FacilitiesDataGrid.ItemsSource = facilities.GetData();
            }
            else
            {
                MessageBox.Show("Выберите помещение!");
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == "" && SpaceTextBox.Text == "" && UserComboBox.SelectedItem == null && InventoryComboBox.SelectedItem == null)
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }
            if (FacilitiesDataGrid.SelectedItem != null)
            {
                object ID = (FacilitiesDataGrid.SelectedItem as DataRowView).Row[0];
                if (UserComboBox.SelectedItem != null)
                {
                    if (InventoryComboBox.SelectedItem != null)
                    {
                        if (nameRegex.IsMatch(NameTextBox.Text))
                        {
                            if (spaceRegex.IsMatch(SpaceTextBox.Text))
                            {
                                int inventoryID = Convert.ToInt32(InventoryComboBox.SelectedValue);
                                int userID = Convert.ToInt32(UserComboBox.SelectedValue);
                                facilities.UpdateQuery(NameTextBox.Text, Convert.ToDouble(SpaceTextBox.Text), userID, inventoryID, Convert.ToInt32(ID));
                                FacilitiesDataGrid.ItemsSource = facilities.GetData();
                            }
                            else
                            {
                                MessageBox.Show("Площадь должна состоять только из цифр и точек!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Название помещения должно содержать только кириллицу и иметь длину от 5 до 50 символов!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите предмет инвентаря!");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите пользователя!");
                }
            }
            else
            {
                MessageBox.Show("Выберите помещение!");
            }
        }

        private void FacilitiesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FacilitiesDataGrid.SelectedItem != null)
            {
                NameTextBox.Text = Convert.ToString((FacilitiesDataGrid.SelectedItem as DataRowView).Row[1]);
                SpaceTextBox.Text = Convert.ToString((FacilitiesDataGrid.SelectedItem as DataRowView).Row[2]);
            }
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CSV Files (*.csv)|*.csv";
            if (dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;
                FileInfo fileInfo = new FileInfo(path);
                long fileSizeInBytes = fileInfo.Length;
                if (fileSizeInBytes / (1024.0 * 1024.0) > 2)
                {
                    MessageBox.Show("Вес импортируемого файла не должен превышать 2-х Мегабайт.");
                }
                else
                {
                    DataTable csvData = ImportData.GetDataTabletFromCSVFile(path);
                    ImportData.InsertDataIntoSQLServerUsingSQLBulkCopy(csvData, "Facilities");
                    FacilitiesDataGrid.ItemsSource = facilities.GetData();
                }
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            DataManipulation.ExportData("facilities", "Facilities");
        }
    }
}
