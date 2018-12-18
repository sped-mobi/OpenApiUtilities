using System;
using System.IO;
using System.Linq;

namespace OpenApiGenerator.Csv
{
    public class CsvStreamReader : StreamReader
    {
        public char CellDelimeter { get; }

        public int RowCount { get; private set; }

        public int Position { get; private set; }


        public static CsvStreamReader From(string filePath, char cellDelimeter = ',')
        {
            return From(new FileStream(filePath, FileMode.Open), cellDelimeter);
        }

        public static CsvStreamReader From(FileStream stream, char cellDelimeter = ',')
        {
            return new CsvStreamReader(stream, cellDelimeter);
        }

        private CsvStreamReader(FileStream stream) : base(stream)
        {
        }

        private CsvStreamReader(FileStream stream, char cellDelimeter) : this(stream)
        {
            CellDelimeter = cellDelimeter;
        }

        /// <summary>Reads a line of characters from the current stream and returns the data as a string.</summary>
        /// <returns>The next line from the input stream, or <see langword="null" /> if the end of the input stream is reached.</returns>
        /// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        public override string ReadLine()
        {
            RowCount++;
            return base.ReadLine();
        }

        public CsvRow ReadRow()
        {
            return new CsvRow();
        }

        private void Advance()
        {
            ReadChar();
        }

        private char PeekChar()
        {
            int read = Peek();
            if (read == -1)
            {
                return char.MaxValue;
            }

            Position++;
            return Convert.ToChar(read);
        }

        private char ReadChar()
        {
            int read = Read();
            if (read == -1)
            {
                return char.MaxValue;
            }

            Position++;
            return Convert.ToChar(read);
        }

        //public CsvRow ReadRow()
        //{
        //    string line = ReadLine();
        //    CsvRow row = new CsvRow();
        //    if (!string.IsNullOrEmpty(line))
        //    {
        //        row.Index = RowCount - 1;
        //        row.RowNumber = RowCount;



        //        string[] array = line.Split(CellDelimeter);
        //        for (int i = 0; i < array.Length; i++)
        //        {
        //            int position = i + 1;
        //            var cell = new CsvCell
        //            {
        //                Position = position,
        //                Value = array[i]
        //            };
        //            row.Cells.Add(cell);
        //        }

        //        return row;
        //    }

        //    return null;
        //}

        public CsvTable ReadCsv()
        {
            CsvTable table = new CsvTable();

            CsvRow current = null;

            while ((current = ReadRow()) != null)
            {
                table.Rows.Add(current);
            }

            return table;
        }

        private string EscapeText(string text)
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
    }
}
