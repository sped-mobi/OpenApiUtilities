using System.IO;
using System.Linq;
using System.Text;

namespace OpenApiGenerator.Csv
{
    public class CsvTextWriter : TextWriter
    {
        public static CsvTextWriter Create(string filePath) => new CsvTextWriter(filePath);

        public CsvTextWriter(string filePath)
            : this(new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
        {
        }

        public CsvTextWriter(FileStream fileStream)
        {
            InnerWriter = new StreamWriter(fileStream);
        }

        public char CellDelimeter => ',';

        public string RowDelimeter => "\r\n";

        public FileStream Stream
        {
            get
            {
                return InnerWriter.BaseStream as FileStream;
            }
        }

        public override Encoding Encoding
        {
            get
            {
                return Encoding.GetEncoding("utf-16");
            }
        }

        public StreamWriter InnerWriter { get; }

        public string EscapeText(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                if (text.Contains(CellDelimeter)
                    || text.Contains("\"")
                    || text.Contains("\r\n")
                    || text.Contains("\n"))
                {
                    text = text.Replace("\"", "\"\"");
                    text = '"' + text + '"';
                }
            }

            return text;
        }

        public void WriteCsvRow(params object[] values)
        {
            if (values != null && values.Length > 0)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    string value = values[index].ToString();
                    WriteCsvCell(value, index == values.Length - 1);
                }
            }
        }

        public void WriteCsvRow(params string[] values)
        {
            if (values != null && values.Length > 0)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    string value = values[index];
                    WriteCsvCell(value, index == values.Length - 1);
                }
            }
        }

        public void WriteCsvCell(string value, bool last = false)
        {
            string escapedValue = EscapeText(value);
            Write(escapedValue);
            if (!last)
            {
                Write(CellDelimeter);
            }
            else
            {
                Write(RowDelimeter);
            }
        }

        public override void Write(string value)
        {
            InnerWriter.Write(value);
        }

        public override void Write(char value)
        {
            InnerWriter.Write(value);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                InnerWriter.Dispose();
                base.Dispose(true);
            }
        }
    }
}