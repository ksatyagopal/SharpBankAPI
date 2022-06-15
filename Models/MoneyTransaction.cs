using System;
using System.Collections.Generic;

#nullable disable

namespace SharpBankAPI.Models
{
    public partial class MoneyTransaction
    {
        public int TransactionId { get; set; }
        public int TaccountNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public double TransactionAmount { get; set; }
        public double CurrentBalance { get; set; }

        public virtual BankAccount TaccountNumberNavigation { get; set; }
    }
}
