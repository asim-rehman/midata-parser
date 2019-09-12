using MiData.Core;
using MiData.IO;
using MiData.Models;
using System;
using System.Collections.Generic;

namespace MiData
{
    public class Command : ICommand
    {
        private const string COMMAND_MIDATA = "midata";
        private const string COMMAND_EXIT = "exit";
        private const string COMMAND_HELP = "help";
        private const string COMMAND_CLEAR = "clear";
        private const string COMMAND_SOURCE = "source";
        private const string COMMAND_OUTPUT = "output";
        private const string COMMAND_DELIMITER = "delimiter";
        private const string COMMAND_TYPE = "type";
        public void Execute(string command)
        {
            switch (command)
            {
                case COMMAND_MIDATA:
                    TransactionReader transactionParser = new TransactionReader();
                    List<TransactionsList> data = transactionParser.ReadFileAsync(Settings.GetSettings.InputPath).Result;
                    if (data != null)
                    {
                        TransactionWriter transactionWriter = new TransactionWriter();
                        if (Settings.GetSettings.Format == FileFormat.Excel)
                            transactionWriter.CreateExcelFile(data, Settings.GetSettings.OutputPath);
                        else
                            transactionWriter.CreateCSVFile(data, Settings.GetSettings.OutputPath);
                    }
                    break;
                case COMMAND_HELP:
                    ShowHelp();
                    break;
                case COMMAND_EXIT:
                    Environment.Exit(0);
                    break;
                case COMMAND_CLEAR:
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Invalid Command");
                    ShowHelp();
                    break;
            }
        }
        private List<Commands> Commands()
        {
            List<Commands> commands = new List<Commands>();
            commands.Add(new Commands
            {
                Name=COMMAND_MIDATA,
                Description = "Parse a MiData File"
            });
            commands.Add(new Commands
            {
                Name = COMMAND_EXIT,
                Description = "Exit the application"
            });
            commands.Add(new Commands
            {
                Name = COMMAND_HELP,
                Description = "Shows availble commmands"
            });
            commands.Add(new Commands
            {
                Name = COMMAND_CLEAR,
                Description = "Clear the console"
            });
            commands.Add(new Commands
            {
                Name = COMMAND_SOURCE,
                Description = "Set source file destination"
            });
            commands.Add(new Commands
            {
                Name = COMMAND_OUTPUT,
                Description = "Set output destination"
            });
            commands.Add(new Commands
            {
                Name = COMMAND_DELIMITER,
                Description = "Set delimiter"
            });
            commands.Add(new Commands
            {
                Name = COMMAND_TYPE,
                Description = "Set file type (excel/csv)"
            });
            return commands;
        }

        private void ShowHelp()
        {
            Console.WriteLine("Availble Commands");
            foreach(var item in Commands())
            {
                Console.WriteLine(item.Name + "\t" + item.Description);
            }
        }
    }
}
