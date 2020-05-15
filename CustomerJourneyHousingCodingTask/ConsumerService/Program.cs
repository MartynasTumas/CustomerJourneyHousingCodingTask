using System;
using System.Data;
using TaxCalculationService;
namespace ConsumerService
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "TaxData.csv";//add path of the file
            TaxCalculationService.Program.InsertTaxesFromFile(path);

            bool run = true;
            while (run)
            {
                Console.WriteLine("\r\nEnter number to select action.\r\nGet tax based on municipatily and date: 1\r\n" +
                    "Insert tax from file: 2\r\nInsert new tax: 3\r\nClose program: 4");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("Enter municipality and click enter");
                        string municipality = Console.ReadLine();
                        Console.WriteLine("Enter tax start date in this format: 2016-12-25 and click enter");
                        DateTime startDate = new DateTime();
                        try
                        {
                            startDate = DateTime.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Incorrect date input.");
                            break;
                        }

                        Console.WriteLine("Enter tax end date (if there is no end date, just click enter) in this format: 2016-12-25 and click enter");
                        DateTime endDate = new DateTime();
                        input = Console.ReadLine();
                        if (!string.IsNullOrEmpty(input))
                        {
                            try
                            {
                                endDate = DateTime.Parse(input);
                            }
                            catch
                            {
                                Console.WriteLine("Incorrect date input.");
                                break;
                            }
                        }


                        DataTable taxes = TaxCalculationService.Program.AskForTax(municipality, startDate, endDate);

                        if (taxes.Rows.Count < 1)
                        {
                            Console.WriteLine("Sorry, no results");
                        }
                        else
                        {
                            foreach (DataRow taxRow in taxes.Rows)
                            {
                                Console.WriteLine("Municipality: " + taxRow["Municipality"].ToString() +
                                  "\r\nTax: " + taxRow["Tax"].ToString() +
                                  "\r\nStart date: " + taxRow["StartDate"].ToString() +
                                  "\r\nEnd date: " + taxRow["EndDate"].ToString());
                            }
                        }
                        break;

                    case "2":
                        //TODO
                        break;

                    case "3":
                        //TODO
                        break;

                    case "4":
                        run = false;
                        break;

                    default:

                        Console.WriteLine("Incorrect input");
                        break;

                }
            }
        }
    }
}
