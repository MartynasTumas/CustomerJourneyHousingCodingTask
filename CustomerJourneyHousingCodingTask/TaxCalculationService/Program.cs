using Microsoft.VisualBasic.FileIO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace TaxCalculationService
{
    public class Program
    {
        private static string connectionString = @"Server=DESKTOP-4DG3M6E\SQLEXPRESS; Database = MunicipalityTax; Trusted_Connection=true;";
        public static void Main()
        {

            //  InsertTaxesFromFile();
           // AskForTax("Vilnius", new DateTime(2016,01,01));

        }

        public static DataTable AskForTax(string municipality, DateTime startDate, DateTime endDate = new DateTime())
        {

            string commandText = "SELECT * FROM Tax WHERE Municipality=@Municipality AND StartDate=@StartDate";

            //end date is optional
            if (endDate.Ticks != 0)
                commandText += " AND EndDate=@EndDate";

            DataTable resultDt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {

                        command.Connection = connection;
                        command.CommandText = commandText;
                        command.Parameters.AddWithValue("@Municipality", municipality);
                        command.Parameters.AddWithValue("@StartDate", startDate).SqlDbType = SqlDbType.DateTime;
                        if (endDate.Ticks != 0)
                            command.Parameters.AddWithValue("@EndDate", endDate);

                        try
                        {
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                            // this will query your database and return the result to your datatable
                            dataAdapter.Fill(resultDt);
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                return resultDt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not open connection. " + ex.Message);
            }
            return null;
        }

        public static void InsertTaxesFromFile(string path)
        {
            DataTable dataFromFile = GetDataTableFromCSVFile(path);

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlBulkCopy s = new SqlBulkCopy(connection))
                    {
                        s.DestinationTableName = "Tax";
                        foreach (var column in dataFromFile.Columns)
                            s.ColumnMappings.Add(column.ToString(), column.ToString());
                        s.WriteToServer(dataFromFile);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not open connection. " + ex.Message);
            }
        }

        public static void InsertNewTax(string municipality, double tax, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrEmpty(municipality))
            {
                Console.WriteLine("Error. Municipality is not provided");
            }
            else if (tax == 0)
            {
                Console.WriteLine("Error. Tax is not provided");
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        string commandText = "INSERT INTO Tax (Municipality, Tax, StartDate, EndDate) VALUES (@Municipality, @Tax, @StartDate, @EndDate)";
                        command.Connection = connection;
                        command.CommandText = commandText;
                        command.Parameters.AddWithValue("@Municipality", municipality);
                        command.Parameters.AddWithValue("@Tax", tax);
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }

        private static DataTable GetDataTableFromCSVFile(string filePath)
        {
            DataTable fileData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(filePath))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        fileData.Columns.Add(datecolumn);
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
                        fileData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading CSV file. " + ex.Message);
                return null;
            }
            return fileData;
        }
    }
}
