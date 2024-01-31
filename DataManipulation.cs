using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Office.Interop.Excel;

namespace AssetsIS
{
    public class DataManipulation
    {
        private const string getConnection = @"data source=DESKTOP-CQ7380I;initial catalog=master;trusted_connection=true";
        public static void ExportData(string fileName, string TableName)
        {
            string query = $"SELECT * from AssetsData.dbo.{TableName}";
            string path;
            CommonOpenFileDialog dialog = new CommonOpenFileDialog { IsFolderPicker = true };
            var result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                path = dialog.FileName;
                MessageBox.Show($"Выбранная директория: {path}");
                ChooseFormatWindow chooseFormatWindow = new ChooseFormatWindow();
                chooseFormatWindow.ShowDialog();
                if (chooseFormatWindow.isCSV == true)
                {
                    using (SqlConnection connection = new SqlConnection(getConnection))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        connection.Open();

                        System.Data.DataTable dataTable = new System.Data.DataTable();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }

                        WriteDataTableToCSV(dataTable, path + "\\" + fileName + ".csv");
                    }
                }
                else if (chooseFormatWindow.isCSV == false)
                {
                    using (SqlConnection connection = new SqlConnection(getConnection))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        connection.Open();

                        System.Data.DataTable dataTable = new System.Data.DataTable();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                            Workbook excelWorkbook = excelApp.Workbooks.Add();
                            Worksheet excelWorksheet = excelWorkbook.Worksheets[1];

                            // Добавление названия столбцов в файл
                            int col = 1;
                            foreach (DataColumn column in dataTable.Columns)
                            {
                                excelWorksheet.Cells[1, col].Value2 = column.ColumnName;
                                col++;
                            }

                            // Добавление данных в файл
                            for (int i = 0; i < dataTable.Rows.Count; i++)
                            {
                                DataRow dataRow = dataTable.Rows[i];
                                for (int j = 0; j < dataTable.Columns.Count; j++)
                                {
                                    excelWorksheet.Cells[i + 2, j + 1].Value2 = dataRow[j];
                                }
                            }

                            excelWorkbook.SaveAs(path + "\\" + fileName + ".xlsx");
                            excelWorkbook.Close();
                            excelApp.Quit();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите директорию, в которую вы хотите экспортировать файл.");
            }
        }

        private static void WriteDataTableToCSV(System.Data.DataTable dataTable, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    string[] fields = row.ItemArray.Select(field => field.ToString()).ToArray();
                    writer.WriteLine(string.Join(",", fields));
                }
            }
        }
    }
}
