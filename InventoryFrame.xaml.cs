using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using AssetsIS.AssetsDataDataSetTableAdapters;
using Microsoft.Win32;

namespace AssetsIS
{

    public class Item
    {
        public bool Status { get; set; }
        public string Name { get; set; }
    }

    public partial class InventoryFrame : Page
    {
        InventoryTableAdapter inventory = new InventoryTableAdapter();
        const string PriceMask = @"[+]?\d*\.?\d+"; // Маска для чисел Double
        readonly Regex priceRegex = new Regex(PriceMask);
        readonly Regex nameRegex = new Regex("^[0-9a-zA-Zа-яА-Я]{5,50}$");
        public InventoryFrame()
        {
            List<Item> items = new List<Item>
            {
                new Item { Status = true, Name = "Эксплуатируется" },
                new Item { Status = false, Name = "Не Эксплуатируется" }
            };

            InitializeComponent();
            InventoryDataGrid.ItemsSource = inventory.GetData();
            InventoryDataGrid.IsReadOnly = true;
            StatusComboBox.ItemsSource = items;
            StatusComboBox.SelectedValuePath = "Status";
            StatusComboBox.DisplayMemberPath = "Name";
        }

        private void InventoryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InventoryDataGrid.SelectedItem != null)
            {
                NameTextBox.Text = Convert.ToString((InventoryDataGrid.SelectedItem as DataRowView).Row[1]);
                PriceTextBox.Text = Convert.ToString((InventoryDataGrid.SelectedItem as DataRowView).Row[2]);
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (StatusComboBox.SelectedItem == null && NameTextBox.Text == "" && PriceTextBox.Text == "")
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }
            if (StatusComboBox.SelectedItem != null)
            {
                if (nameRegex.IsMatch(NameTextBox.Text))
                {
                    if (priceRegex.IsMatch(PriceTextBox.Text))
                    {
                        inventory.InsertQuery(NameTextBox.Text, Convert.ToDouble(PriceTextBox.Text), Convert.ToBoolean(StatusComboBox.SelectedValue));
                        InventoryDataGrid.ItemsSource = inventory.GetData();
                    }
                    else
                    {
                        MessageBox.Show("Цена должна состоять только из цифр и точек.");
                    }
                }
                else
                {
                    MessageBox.Show("Название должно состоять только из цифр, кириллицы и латиницы, а также содержать 5 - 50 символов.");
                }
            }
            else
            {
                MessageBox.Show("Выберите статус!");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (InventoryDataGrid.SelectedItem != null)
            {
                var id = (InventoryDataGrid.SelectedItem as DataRowView).Row[0];
                inventory.DeleteQuery(Convert.ToInt32(id));
                InventoryDataGrid.ItemsSource = inventory.GetData();
            }
            else
            {
                MessageBox.Show("Выберите предмет из таблицы!");
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (StatusComboBox.SelectedItem == null && NameTextBox.Text == "" && PriceTextBox.Text == "")
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }
            else if (InventoryDataGrid.SelectedItem != null)
            {
                object ID = (InventoryDataGrid.SelectedItem as DataRowView).Row[0];
                if (StatusComboBox.SelectedItem != null)
                {
                    if (nameRegex.IsMatch(NameTextBox.Text))
                    {
                        if (priceRegex.IsMatch(PriceTextBox.Text))
                        {
                            inventory.UpdateQuery(NameTextBox.Text, Convert.ToDouble(PriceTextBox.Text), Convert.ToBoolean(StatusComboBox.SelectedValue), Convert.ToInt32(ID));
                            InventoryDataGrid.ItemsSource = inventory.GetData();
                        }
                        else
                        {
                            MessageBox.Show("Цена должна состоять только из цифр и точек.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Название должно состоять только из цифр, кириллицы и латиницы, а также содержать 5 - 50 символов.");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите статус!");
                }
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
                    ImportData.InsertDataIntoSQLServerUsingSQLBulkCopy(csvData, "Inventory");
                    InventoryDataGrid.ItemsSource = inventory.GetData();
                }
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            DataManipulation.ExportData("inventory", "Inventory");
        }
    } 
}
