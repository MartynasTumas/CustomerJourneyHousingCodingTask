using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TaxService.Models;

namespace TaxService.DataAccess
{
    public class TaxAccessLayer : ITaxAccessLayer
    {
        MunicipalityTaxContext context;

        public TaxAccessLayer(MunicipalityTaxContext context)
        {
            this.context = context;
        }

        public List<Tax> GetAllTaxes()
        {
            try
            {
                List<Tax> taxes = context.Tax.ToList();
                return taxes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetSpecificTax(string municipality, DateTime date, string sortBy)
        {
            try
            {
                switch (sortBy)
                {
                    case "yearly":
                        {
                            Tax tax = context.Tax.Where(x => x.Type.ToLower() == sortBy.ToLower() && x.Municipality == municipality).FirstOrDefault();

                            if (tax != null)
                                return tax.TaxAmount.ToString();
                            break;
                        }
                    case "monthly":
                        {
                            Tax tax = context.Tax.Where(x => x.Type.ToLower() == sortBy.ToLower() && x.Municipality == municipality &&
                            x.StartDate.Month == date.Month).FirstOrDefault();

                            if (tax != null)
                                return tax.TaxAmount.ToString();
                            break;
                        }
                    case "weekly":
                        {
                            break;
                        }
                    case "daily":
                        {
                            Tax tax = context.Tax.Where(x => x.Type.ToLower() == sortBy.ToLower() && x.Municipality == municipality &&
                            x.StartDate.DayOfYear == date.DayOfYear).FirstOrDefault();

                            if (tax != null)
                                return tax.TaxAmount.ToString();
                            break;
                        }
                }

                return "Error";
            }
            catch
            {
                return "Error";
            }
        }

        public void AddNewTax(Tax newTax)
        {
            context.Tax.Add(newTax);
            context.SaveChanges();
        }

        public void AddFromFile(string path)
        {
            DataTable data = GetDataTableFromCSVFile(path);//(@"C:\Users\Martynas\source\repos\CustomerJourneyHousingCodingTask\TaxData.csv");

            foreach (DataRow row in data.Rows)
            {
                Tax newTax = new Tax()
                {
                    Municipality = row["Municipality"].ToString(),
                    TaxAmount = double.Parse(row["TaxAmount"].ToString()),
                    StartDate = DateTime.Parse(row["StartDate"].ToString()),
                    Type = row["Type"].ToString()
                };

                if (!string.IsNullOrEmpty(row["EndDate"].ToString()))
                    newTax.EndDate = DateTime.Parse(row["EndDate"].ToString());
            
                context.Add(newTax);
                context.SaveChanges();
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
