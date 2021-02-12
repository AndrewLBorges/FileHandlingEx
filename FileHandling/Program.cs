using System;
using System.IO;
using System.Globalization;
using System.Text;

namespace FileHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter file full path: ");
            string sourceFilePath = @Console.ReadLine();

            try
            {
                string[] lines = File.ReadAllLines(sourceFilePath);

                StringBuilder sb = new StringBuilder();
                sb.Append(Path.GetDirectoryName(sourceFilePath));
                sb.Append(@"\out");

                Directory.CreateDirectory(sb.ToString());

                sb.Append(@"\summary.csv");

                using (StreamWriter sw = File.AppendText(sb.ToString()))
                {
                    foreach(string line in lines)
                    {
                        string[] fields = line.Split(',');

                        string name = fields[0];
                        double price = double.Parse(fields[1], CultureInfo.InvariantCulture);
                        int quantity = int.Parse(fields[2]);

                        Product p = new Product(name, price, quantity);

                        sw.WriteLine($"{p.Name},{p.TotalValue().ToString("f2", CultureInfo.InvariantCulture)}");
                    }
                }
            }
            catch(IOException e)
            {
                Console.WriteLine("An erro occured:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
