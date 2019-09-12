using MiData.Core;
using MiData.IO;
using MiData.Models;
using System;
using System.Collections.Generic;

namespace MiData.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" ============= Santander CSV Statement Reader ============= ");

            try
            {
                while (true)
                {
                    Console.Write("> ");
                    switch (Console.ReadLine())
                    {
                        case "go":
                            TransactionReader transactionParser = new TransactionReader();
                            List<TransactionsList> data = transactionParser.ReadFileAsync(Settings.GetSettings.InputPath).Result;

                            if (data != null)
                            {
                                TransactionWriter transactionWriter = new TransactionWriter();
                                transactionWriter.CreateExcelFile(data, Settings.GetSettings.OutputPath);
                            }
                            Console.WriteLine();
                            break;
                        case "exit":
                            Environment.Exit(0);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (UnauthorizedAccessException uae)
            {
                Console.WriteLine("Permission denied to directory, please try running as administrator." + uae.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
