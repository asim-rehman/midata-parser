using System;
using System.Collections.Generic;

namespace MiData.Models
{
    public class TransactionsList
    {

        public TransactionsList()
        {
            TransactionEntity = new List<TransactionEntity>();
        }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<TransactionEntity> TransactionEntity { get; set; }
    }
}
