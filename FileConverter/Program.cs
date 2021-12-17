using System;
using Fclp;
using FileConverter.Service;
using FileConverter.Service.Models;

namespace FileConverter
{
    class Program
    {
        private static readonly string VERTICAL_WHITE_SPACE = Environment.NewLine + Environment.NewLine;
        private static readonly string NEW_LINE = Environment.NewLine;

        private static string helpText = "HELP";

        private static readonly ICsvService _csvService = new CsvService();

        static void Main(string[] args)
        {
            var p = new FluentCommandLineParser<ApplicationArguments>();

            p.Setup(arg => arg.From)
                .As('e', "extension")
                .Required()
                .WithDescription("File Extension");

            p.Setup(arg => arg.To)
                .As('t', "to")
                .Required();

            p.Setup(arg => arg.Filename)
                .As('f', "filename")
                .Required();

            p.Setup(arg => arg.FromDatabase)
                .As('d', "database");

            p.SetupHelp("?", "help")
                .Callback(text => System.Console.WriteLine(text));

            //TODO: Prevent same From/To file type.
            //TODO: Get File Extension from Filename and remove that param.

            var parsed = p.Parse(args);

            var validatedInput = p.Object;

            if (parsed.HasErrors == false)
            {
                if (parsed.HelpCalled)
                {
                    System.Console.WriteLine(PrintHelp());
                    System.Console.ReadLine();
                }
                else
                {
                    var result = _csvService.ParseFile(validatedInput);

                    if (result == string.Empty)
                    {
                        System.Console.WriteLine($"File not Found! { validatedInput.Filename }");
                    }

                    System.Console.WriteLine(result);
                    System.Console.ReadLine();
                }
            }
            else
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("INCORRECT OR NO PARAMETERS ADDED");
                System.Console.ResetColor();
                System.Console.WriteLine(PrintHelp());
                System.Console.ReadLine();
            }
        }

        private static string PrintHelp()
        {
            helpText += VERTICAL_WHITE_SPACE;

            helpText += "usage: ConvertFile [--e, --extension REQUIRED] [--t, --to REQUIRED] [--f, --filename REQUIRED]";
            helpText += NEW_LINE;
            helpText += "                   [--d, --database]";

            return helpText;
        }
    }
}
