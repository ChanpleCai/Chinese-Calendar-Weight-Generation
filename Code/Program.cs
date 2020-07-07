using System;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace Chinese_Calendar_Weight_eneration
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var list = Helper.DateGeneration().DateForwardProcess().DateBackwardProcess();

            using (var writer = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "File.csv")))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(list);
            }
        }
    }
}