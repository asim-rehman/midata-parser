using MiData.Core;
using MiData.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiData.IO
{
    public class TransactionReader : ITransactionReader
    {
        public async Task<List<TransactionsList>> ReadFileAsync(string filePath)
        {
            string[] data = new string[] { };
            int lineCount = 0;
            string messages = string.Empty;

            if (File.Exists(filePath))
            {

                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    string line = await sr.ReadLineAsync();

                    Console.WriteLine("INFO: ============== Reading File ==============");
                    Console.WriteLine("INFO: Delimiter =  " + Settings.GetSettings.Delimiter);
                    List<TransactionsList> transactionsList = new List<TransactionsList>();
                    string[] lines = await File.ReadAllLinesAsync(filePath);
                    lineCount = lines.Count();
                    int index = 1;
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        index++;

                        int percentage = TransactionsHelper.GetPercentage(index, lineCount);
                        ConsoleHelper.Write("INFO: Reading Line " + index + " of " + lineCount + " (" + percentage + "% completed)");

                        data = line.Split(Settings.GetSettings.Delimiter, StringSplitOptions.RemoveEmptyEntries);

                        if (data.Length >= 5)
                        {
                            TransactionEntity transactionEntity = new TransactionEntity();
                            DateTime transactionDate = new DateTime();

                            transactionEntity.TransactionDate = data[0].Clean().ConvertStringToDate();
                            transactionEntity.TransactionType = data[1].Clean();
                            transactionEntity.TransactionDescription = data[2].Clean();
                            transactionEntity.TransactionAmount = data[3].ConvertToDecimal();
                            transactionEntity.TransactionBalance = data[4].ConvertToDecimal();

                            if (transactionEntity.TransactionDate != DateTime.MinValue)
                            {
                                transactionDate = transactionEntity.TransactionDate;
                                string sheetName = transactionDate.ToString("MMM-yyyy");

                                TransactionsList findExisting = transactionsList.Find(t => t.Name == sheetName);

                                if (findExisting != null)
                                {
                                    findExisting.TransactionEntity.Add(transactionEntity);
                                }
                                else
                                {
                                    TransactionsList newMonthlyTransaction = new TransactionsList();
                                    newMonthlyTransaction.Name = sheetName;
                                    newMonthlyTransaction.Date = transactionDate;
                                    newMonthlyTransaction.TransactionEntity.Add(transactionEntity);

                                    transactionsList.Add(newMonthlyTransaction);
                                }
                            }
                            else
                            {
                                messages += "WARNING:Line " + index + " can't parse date and time\n";
                            }


                        }
                        else
                        {
                            messages += "WARNING:Line " + index + " skipped invalid or empty data";
                            if (index != lineCount)
                                messages += "\n";
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine(messages);
                    return transactionsList;
                }

            }
            else
            {
               Console.WriteLine("FATAL ERROR: Can't find file " + filePath);
            }

            return null;
        }

    }
}
