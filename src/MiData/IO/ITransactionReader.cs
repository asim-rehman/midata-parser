using MiData.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiData.IO
{
    public interface ITransactionReader
    {
        Task<List<TransactionsList>> ReadFileAsync(string filePath);
    }
}
