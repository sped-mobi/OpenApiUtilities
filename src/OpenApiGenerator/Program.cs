using System;
using System.IO;
using System.Text;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using System.Windows;
using Microsoft.CodeAnalysis.Text;
using OpenApiGenerator.InternalUtilities;
using OpenApiGenerator.InternalUtilities.CKAN;
using OpenApiGenerator.InternalUtilities.CodeGeneration;
using OpenApiGenerator.Csv;

namespace OpenApiGenerator
{
    internal class Program
    {
        private const string FilePath = @"C:\stage\dataset\utils\OpenApiGenerator\ckan.openapi.json";
        private const string FilePath2 = @"C:\stage\ckan.api\openapi.json";
        private const string CKAN_LIST = @"C:\stage\dataset\utils\OpenApiGenerator\InternalUtilities\CKAN\ckan_lists.csv";

        [STAThread]
        private static void Main(string[] args)
        {
            FileParser parser = new FileParser(SourceText.From(File.ReadAllText(CKAN_LIST)));

            var firstRow = parser.ParseRow();

            Console.ReadKey();
        }

        private static void RunApiWriter()
        {
            var document = CkanApiFactory.Document();

            var sb = new StringBuilder();
            using (FileStream fs = new FileStream(FilePath2, FileMode.Create, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine();
                OpenApiJsonWriter writer = new OpenApiJsonWriter(sw);
                document.SerializeAsV3(writer);

            }

            Console.Write(sb);

            Clipboard.SetText(sb.ToString());


            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        private static void RunApiGenerator()
        {
            var document = CkanApiFactory.Document();

            var sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                sw.WriteLine();
                ApiGenerator.Generate(sw, document);
            }

            Console.Write(sb);

            Clipboard.SetText(sb.ToString());


            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
