using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace CalendarGeneration
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var list = Helper.DateGeneration().DateForwardProcess().DateBackwardProcess().Where(x => x.Year != 2015 && x.Year != 2021).ToList();

            using (var writer = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "File.csv")))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(list);
            }
        }
    }
}