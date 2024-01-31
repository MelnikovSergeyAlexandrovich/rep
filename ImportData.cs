using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AssetsIS
{
    public class ImportData
    {
        private const string getConnection = @"data source=DESKTOP-CQ7380I;initial catalog=master;trusted_connection=true";
        public static DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return csvData;
        }

        public static void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable csvFileData, string TableName)
        {
            using (SqlConnection dbConnection = new SqlConnection(getConnection))
            {
                dbConnection.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
                {
                    s.DestinationTableName = $"AssetsData.dbo.{TableName}";

                    foreach (var column in csvFileData.Columns)
                        s.ColumnMappings.Add(column.ToString(), column.ToString());
                    try
                    {
                        s.WriteToServer(csvFileData);
                    }
                    catch 
                    {
                        MessageBox.Show("Не все поля в файле соответствуют полям в таблице.");
                        return;
                    }
                }
            }
        }

        
        
    }
}
