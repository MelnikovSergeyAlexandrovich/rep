
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
    public partial class AssetsFrame : Page
    {
        PropertyTableAdapter property = new PropertyTableAdapter(); 
        FacilitiesTableAdapter facility = new FacilitiesTableAdapter();
        const string nameMask = @"[0-9а-яА-Я\sa-zA-Z]{5,50}";
        const string locationMask = @"[0-9а-яА-Я\sa-zA-Z,./]{5,50}";
        const string doubleMask = @"[+]?\d*\.?\d+"; // Маска для чисел Double
        readonly Regex nameRegex = new Regex(nameMask);
        readonly Regex locationRegex = new Regex(locationMask); 
        readonly Regex doubleRegex = new Regex(doubleMask);
        public AssetsFrame()
        {
            InitializeComponent();
            AssetsDataGrid.IsReadOnly = true;
            AssetsDataGrid.ItemsSource = property.GetData();
            FacilityComboBox.ItemsSource = facility.GetData();
            FacilityComboBox.SelectedValuePath = "ID";
            FacilityComboBox.DisplayMemberPath = "name";
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (FacilityComboBox.SelectedItem != null && NameTextBox.Text != "" && PriceTextBox.Text != "" && SpaceTextBox.Text != "" && LocationTextBox.Text != "")
            {
                if(FacilityComboBox.SelectedItem != null)
                {
                    if(nameRegex.IsMatch(NameTextBox.Text))
                    {
                        if(locationRegex.IsMatch(LocationTextBox.Text))
                        {
                            if(doubleRegex.IsMatch(SpaceTextBox.Text))
                            {
                                if(doubleRegex.IsMatch(PriceTextBox.Text))
                                {
                                    property.InsertQuery(NameTextBox.Text,Convert.ToDouble(SpaceTextBox.Text),Convert.ToDouble(PriceTextBox.Text),LocationTextBox.Text,Convert.ToInt32(FacilityComboBox.SelectedValue));
                                    AssetsDataGrid.ItemsSource = property.GetData();
                                }
                                else
                                {
                                    MessageBox.Show("Цена должна содержать только цифры и точки");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Площадь должна содержать только цифры и точки");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Локация должна содержать только кириллицу, латиницу, точки, запятые, дроби и иметь длину от 5 до 50 символов!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Название должно содержать только кириллицу и латиницу и иметь длину от 5 до 50 символов!");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите помещение!");
                }
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }
        }

        private void AssetsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AssetsDataGrid.SelectedItem != null)
            {
                NameTextBox.Text = Convert.ToString((AssetsDataGrid.SelectedItem as DataRowView).Row[1]);
                PriceTextBox.Text = Convert.ToString((AssetsDataGrid.SelectedItem as DataRowView).Row[3]);
                SpaceTextBox.Text = Convert.ToString((AssetsDataGrid.SelectedItem as DataRowView).Row[2]);
                LocationTextBox.Text = Convert.ToString((AssetsDataGrid.SelectedItem as DataRowView).Row[4]);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (AssetsDataGrid.SelectedItem != null)
            {
                var id = (AssetsDataGrid.SelectedItem as DataRowView).Row[0];
                property.DeleteQuery(Convert.ToInt32(id));
                AssetsDataGrid.ItemsSource = property.GetData();
            }
            else
            {
                MessageBox.Show("Выберите имущество!");
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (FacilityComboBox.SelectedItem != null && NameTextBox.Text != "" && PriceTextBox.Text != "" && SpaceTextBox.Text != "" && LocationTextBox.Text != "")
            {
                if (AssetsDataGrid.SelectedItem != null)
                {
                    object id = (AssetsDataGrid.SelectedItem as DataRowView).Row[0];
                    if (FacilityComboBox.SelectedItem != null)
                    {
                        if (nameRegex.IsMatch(NameTextBox.Text))
                        {
                            if (locationRegex.IsMatch(LocationTextBox.Text))
                            {
                                if (doubleRegex.IsMatch(SpaceTextBox.Text))
                                {
                                    if (doubleRegex.IsMatch(PriceTextBox.Text))
                                    {
                                        property.UpdateQuery(NameTextBox.Text, Convert.ToDouble(SpaceTextBox.Text), Convert.ToDouble(PriceTextBox.Text), LocationTextBox.Text, Convert.ToInt32(FacilityComboBox.SelectedValue), Convert.ToInt32(id));
                                        AssetsDataGrid.ItemsSource = property.GetData();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Цена должна содержать только цифры и точки");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Площадь должна содержать только цифры и точки");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Локация должна содержать только кириллицу, латиницу, точки, запятые, дроби и иметь длину от 5 до 50 символов!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Название должно содержать только кириллицу и латиницу и иметь длину от 5 до 50 символов!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите помещение!");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите значение из таблицы!");
                }
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены!");
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
                    ImportData.InsertDataIntoSQLServerUsingSQLBulkCopy(csvData, "Property");
                    AssetsDataGrid.ItemsSource = property.GetData();
                }
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            DataManipulation.ExportData("assets", "Property");
        }
    }
}
