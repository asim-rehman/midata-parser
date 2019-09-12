using MiData.Models;
using System.Collections.Generic;

namespace MiData.IO
{
    public interface ITransactionWriter
    {
        void CreateExcelFile(List<TransactionsList> list, string saveDir);
        void CreateCSVFile(List<TransactionsList> list, string saveDir);
    }
}
