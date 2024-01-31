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
    public partial class RolesFrame : Page
    {
        RolesTableAdapter roles = new RolesTableAdapter();
        Regex RoleNameRegex = new Regex("^[а-яА-Я]{5,50}$");

        public RolesFrame()
        {
            InitializeComponent();
            RolesDataGrid.ItemsSource = roles.GetData();
            RolesDataGrid.IsReadOnly = true;
            RolesComboBox.ItemsSource = roles.GetData();
            RolesComboBox.SelectedValuePath = "ID";
            RolesComboBox.DisplayMemberPath = "name";
        }

        private void CreateButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (RolesComboBox.SelectedItem == null && RoleTextBox.Text == "")
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }
            else if (RoleTextBox.Text == "")
            {
                MessageBox.Show("Введите название роли!");
            }
            else if (RoleNameRegex.IsMatch(RoleTextBox.Text))
            {
                roles.InsertQuery(RoleTextBox.Text);
                RolesDataGrid.ItemsSource = roles.GetData();
                RolesComboBox.ItemsSource = roles.GetData();
            }
            else if (!RoleNameRegex.IsMatch(RoleTextBox.Text))
            {
                MessageBox.Show("Название роли было введено неверно. Оно может состоять только из кириллицы и иметь длину в 5-50 символов!");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (RolesComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите роль!");
            }
            else
            {
                int id = Convert.ToInt32(RolesComboBox.SelectedValue);
                roles.DeleteQuery(id);
                RolesDataGrid.ItemsSource = roles.GetData();
                RolesComboBox.ItemsSource = roles.GetData();
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if ((RolesComboBox.SelectedItem == null) && (RoleTextBox.Text == ""))
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }
            else if ((RolesComboBox.SelectedItem != null) && (RoleNameRegex.IsMatch(RoleTextBox.Text)))
            {
                int id = Convert.ToInt32(RolesComboBox.SelectedValue);
                roles.UpdateQuery(RoleTextBox.Text, id);
                RolesDataGrid.ItemsSource = roles.GetData();
                RolesComboBox.ItemsSource = roles.GetData();
            }
            else if ((RolesComboBox.SelectedItem != null) && (!RoleNameRegex.IsMatch(RoleTextBox.Text)))
            {
                MessageBox.Show("Название роли было введено неверно. Оно может состоять только из кириллицы и иметь длину в 5-50 символов!");
            }
            else if ((RolesComboBox.SelectedItem == null) && (RoleNameRegex.IsMatch(RoleTextBox.Text)))
            {
                MessageBox.Show("Выберите роль!");
            }
            
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            DataManipulation.ExportData("roles", "Roles");
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
                    ImportData.InsertDataIntoSQLServerUsingSQLBulkCopy(csvData, "Roles");
                    RolesDataGrid.ItemsSource = roles.GetData();
                }
            }
        }
    }
}
