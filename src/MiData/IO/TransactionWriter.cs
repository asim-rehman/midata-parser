using MiData.Core;
using MiData.Models;
using MiData.Models.Excel;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MiData.IO
{
    public class TransactionWriter : ITransactionWriter
    {
        private string[] headers = new string[] { "Date", "Type", "Description", "Amount", "Balance" };
        public void CreateExcelFile(List<TransactionsList> list, string saveDir)
        {

            if (File.Exists(Settings.GetSettings.ModelFile))
            {
                ExcelModel excelModel = null;
                Console.WriteLine("INFO: ============== Generating Excel File(s) ============== ");
                using (StreamReader sr = new StreamReader(Settings.GetSettings.ModelFile))
                {
                    Console.WriteLine("INFO: Reading Model Structure");
                    string json = sr.ReadToEnd();
                    excelModel = JsonConvert.DeserializeObject<ExcelModel>(json);
                }

                if (excelModel != null)
                {
                    CreateExcelDocument(list, excelModel, saveDir);                    
                }
            }
            else
            {
                Console.WriteLine("FATAL ERROR: ============== Model File not found ============== ");
            }

        }
        public void CreateCSVFile(List<TransactionsList> list, string saveDir)
        {
            int index = 1;
            int percentage = 0;
            foreach (var item in list)
            {
                string savePath = saveDir + "\\" + item.Name + ".csv";
                percentage = TransactionsHelper.GetPercentage(index, list.Count);
                Console.WriteLine("INFO: Creating File " + index + " of " + list.Count + "(" + percentage + "% Completed)\r");
                using (StreamWriter sw = new StreamWriter(savePath))
                {

                    sw.WriteLine(String.Join(Settings.GetSettings.Delimiter, headers));
                    foreach (var entity in item.TransactionEntity)
                    {
                        string line = String.Join(Settings.GetSettings.Delimiter, new string[] {entity.TransactionDate.ToShortDateString(), entity.TransactionType,entity.TransactionDescription,
                                entity.TransactionAmount.ToString(),entity.TransactionBalance.ToString() });
                        sw.WriteLine(line);
                    }
                }

                index++;
            }
        }

        private void CreateExcelDocument(List<TransactionsList> list, ExcelModel excelModel, string saveDir)
        {
            string savePath = string.Empty;
            string format = "yyyy/MM/dd";
            int percentage = 0;
            int index = 1;

            foreach (var sheet in list)
            {
                using (ExcelPackage excelPackage = new ExcelPackage())
                {

                    savePath = saveDir + "\\" + sheet.Name + ".xlsx";
                    percentage = TransactionsHelper.GetPercentage(index, list.Count);
                    string message = "INFO: Creating File " + index + " of " + list.Count + " (" + percentage + "% Completed)";
                    ConsoleHelper.Write(message);
                    foreach (var entity in sheet.TransactionEntity)
                    {
                        char delimiter = Settings.GetSettings.Delimiter;
                        string text = 
                            entity.TransactionDate.ToString(format) + delimiter + 
                            entity.TransactionType + delimiter + 
                            entity.TransactionDescription + delimiter +     
                            entity.TransactionAmount + delimiter +
                            entity.TransactionBalance;

                        ExcelTextFormat excelTextFormat = new ExcelTextFormat();
                        excelTextFormat.Delimiter = Settings.GetSettings.Delimiter;

                        int row = 1;
                        string name = "Other" + sheet.Name;
                        Sheets excelSheet = excelModel.Sheets.Where(p => p.Items.Any(entity.TransactionType.ToLower().Contains) 
                        || p.Items.Any(entity.TransactionDescription.ToLower().Contains)).FirstOrDefault();
                        if (excelSheet != null)
                        {
                            if (excelPackage.Workbook.Worksheets[excelSheet.Name] == null)
                            {
                                excelPackage.Workbook.Worksheets.Add(excelSheet.Name);
                            }

                            name = excelSheet.Name;
                        }
                        else
                        {
                            if (excelPackage.Workbook.Worksheets[name] == null)
                            {
                                excelPackage.Workbook.Worksheets.Add(name);
                            }
                        }

                        var worksheet = excelPackage.Workbook.Worksheets[name];
                        if (worksheet.Dimension == null)
                            row = 1;
                        else
                            row = worksheet.Dimension.End.Row + 1;

                        worksheet.Cells[row, 1, row, headers.Length].LoadFromText(text, excelTextFormat);
                        worksheet.Cells[row, 1].Style.Numberformat.Format = format;
                    }

                    FileInfo excelFile = new FileInfo(savePath);
                    excelPackage.SaveAs(excelFile);
                    index++;
                }
            }
            Console.WriteLine("INFO: " + percentage + "% Completed, Saved file(s) to directory ");
        }
    }
}
