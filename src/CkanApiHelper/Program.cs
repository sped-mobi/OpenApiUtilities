using CkanApiHelper.Core;
using Newtonsoft.Json;
using QuickType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CkanApiHelper
{
    internal class Program
    {
        public const string ApiKey = "2c2e5e7f-8982-45a7-bf05-03618bed4f23";
        public const string FilePath = @"C:\stage\dataset\utils\CkanApiHelper\Configuration.json";

        [STAThread]
        public static void Main(string[] args)
        {
            Configuration config = Configuration.FromJson(File.ReadAllText(FilePath));

            StringBuilder sb = new StringBuilder();
            using (StringWriter writer = new StringWriter(sb))
            {
                ApiGenerator.Generate(writer, config);
            }

            Console.Write(sb);

            Clipboard.SetText(sb.ToString());


            //var result = Api.OrganizationList();



        }
    }
}
