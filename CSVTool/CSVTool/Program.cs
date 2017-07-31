using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVTool
{
    class Program
    {
        private static string CSVUrl;
        static void Main(string[] args)
        {
            //args = new string[1];
            //args[0] = @"E:\Mine\KBEGameDir\KBEGame\CSVTool\CSVTool\bin\Release\CSVDir\ppp.csv";
            if (args.Length < 1)
                return;
            ParseArg(args);
            Proccess();
        }

        static void ParseArg(string[] args)
        {
            CSVUrl = args[0];
        }

        static void Proccess()
        {
            CsvStreamReader reader = new CsvStreamReader(CSVUrl);
        }
    }
}
