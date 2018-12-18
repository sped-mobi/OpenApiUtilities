using QuickType;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CkanApiHelper
{
    public static class ApiGenerator
    {
        private static IndentedTextWriter Writer { get; set; }


        public static void Generate(TextWriter writer, Configuration config)
        {
            Writer = new IndentedTextWriter(writer);

            WriteLine();
            WriteLine();

            PushIndent();

            foreach (var @class in config.Config)
            {
                string className = Proper(@class.Name);

                //WriteLine();
                //WriteLine($"public partial class {className}");
                //OpenBlock();

                WriteLine($"Api.Path(\"/{@class.Name}\",");
                WriteLine($"    Api.PathItem(OperationFactory.OperationMap(\"{className}\"))),");

                foreach (var prop in @class.Properties)
                {
                    if (string.IsNullOrEmpty(prop.Type))
                        continue;




                    //string type = prop.Type?.Replace("strings", "IList<string>")
                    //    .Replace("dictionaries", "object")
                    //    .Replace("dict", "object");

                    //WriteLine();
                    //WriteLine($"public {type} {Proper(prop.Name)} {{ get; set; }}");
                }

                //CloseBlock();
            }

        }


        private static void OpenBlock()
        {
            WriteLine("{");
            PushIndent();
        }

        private static void CloseBlock()
        {
            PopIndent();
            WriteLine("}");
        }

        private static string Proper(string text)
        {
            
            if (text.Contains("_"))
            {
                StringBuilder sb = new StringBuilder();
                foreach (var section in text.Split('_'))
                {
                    sb.Append(CodeIdentifier.MakePascal(section));
                }

                return sb.ToString();
            }
            else
            {
                return CodeIdentifier.MakePascal(text);
            }
        }

        private static void PushIndent() => Writer.Indent++;
        private static void PopIndent() => Writer.Indent--;
        private static void Write(string value) => Writer.Write(value);
        private static void WriteLine(string value) => Writer.WriteLine(value);
        private static void WriteLine() => Writer.WriteLine();
    }
}
