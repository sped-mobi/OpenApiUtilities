// -----------------------------------------------------------------------
// <copyright file="FileParser.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Data;
using System.Windows.Media;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities.Lexer;

namespace OpenApiGenerator.Csv
{
    public class FileParser : AbstractLexer
    {
        public FileParser(SourceText sourceText) : base(sourceText)
        {
        }

        private int RowCount;

        public CsvRow ParseRow()
        {
            var lexer = TextWindow;

            RowCount++;

            CsvRow row = new CsvRow();

            Start();
            
            top:

            char current = lexer.PeekChar();

            List<string> values = new List<string>();

            while (current != '\n')
            {
                Start();

                lexer.AdvanceChar();
                current = lexer.PeekChar();

                if (current == '"')
                {
                    if (lexer.PeekChar(1) == '"')
                    {
                        lexer.AdvanceChar(1);
                    }
                    else
                    {
                        char c;
                        while ((c = lexer.PeekChar()) != '"')
                        {
                            lexer.AdvanceChar();
                            c = lexer.PeekChar();
                            if (c == '"' && lexer.PeekChar(1) == ',')
                            {
                                lexer.AdvanceChar();
                                values.Add(lexer.GetInternedText());
                                goto top;
                            }

                        }
                    }
                }
                else if (current == char.MaxValue)
                {
                    return null;
                }
                else
                {
                    

                    while (current != ',')
                    {
                        lexer.AdvanceChar();
                        current = lexer.PeekChar();
                        if (current == '"')
                        {
                            if (lexer.PeekChar(1) == '"')
                            {
                                lexer.AdvanceChar(1);
                            }
                            else if (lexer.PeekChar(1) == ',')
                            {
                                lexer.AdvanceChar(1);
                                goto exit;
                            }

                            lexer.AdvanceChar();
                            current = lexer.PeekChar();
                        }

                        if (current == SlidingTextWindow.InvalidCharacter)
                        {
                            goto exit;
                        }

                    }

                    exit:

                    values.Add(lexer.GetInternedText());
                }
            }

            

            for (int i = 0; i < values.Count; i++)
            {
                string value = values[i];
                CsvCell cell = new CsvCell();
                cell.Position = i + 1;
                cell.Value = value;
                row.Cells.Add(cell);
            }

            row.Index = RowCount - 1;
            row.RowNumber = RowCount;

            return row;
        }



    }
}
