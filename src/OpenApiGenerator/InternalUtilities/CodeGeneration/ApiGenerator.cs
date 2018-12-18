// -----------------------------------------------------------------------
// <copyright file="ApiGenerator.cs" company="sped.mobi">
//     Copyright © 2018 Brad R. Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using Microsoft.OpenApi.Models;

namespace OpenApiGenerator.InternalUtilities.CodeGeneration
{
    public static class ApiGenerator
    {
        private static IndentedTextWriter Writer { get; set; }


        public static void Generate(TextWriter writer, OpenApiDocument document)
        {
            Writer = new IndentedTextWriter(writer);

            WriteLine();
            WriteLine();

            PushIndent();

            WriteLine("public static class Api");
            OpenBlock();

            foreach (var documentPath in document.Paths)
            {
                foreach (var operation in documentPath.Value.Operations)
                {
                    WriteLine();
                    WriteLine("/// <summary>");
                    WriteLine($"/// {operation.Value.Description}");
                    WriteLine("/// </summary>");
                    WriteLine($"public static async Task<IActionResult> {operation.Value.OperationId}()");
                    OpenBlock();
                    WriteLine("await Task.CompletedTask;");
                    WriteLine("return null;");
                    CloseBlock();
                }
            }


            CloseBlock();

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

        private static void PushIndent() => Writer.Indent++;
        private static void PopIndent() => Writer.Indent--;
        private static void Write(string value)
        {
            Writer.Write(value);
        }

        private static void WriteLine(string value) => Writer.WriteLine(value);
        private static void WriteLine() => Writer.WriteLine();
    }
}
