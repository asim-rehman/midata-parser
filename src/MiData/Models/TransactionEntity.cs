using System;

namespace MiData.Models
{
    /// <summary>
    /// A class which holds information on a single transaction
    /// </summary>
    public class TransactionEntity
    {
        /// <summary>
        /// The date of the transaction
        /// </summary>
        public DateTime TransactionDate { get; set; }
        /// <summary>
        /// The type of transaction (expense/income)
        /// </summary>
        public string TransactionType { get; set; }
        /// <summary>
        /// Description of transaction
        /// </summary>
        public string TransactionDescription { get; set; }
        /// <summary>
        /// The transaction amount
        /// </summary>
        public decimal TransactionAmount { get; set; }
        /// <summary>
        /// The remaining balance
        /// </summary>
        public decimal TransactionBalance { get; set; }
    }
}
