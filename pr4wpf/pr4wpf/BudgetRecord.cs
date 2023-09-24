using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr4wpf
{
    internal class BudgetRecord
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public bool IsIncome { get; set; }
        public DateTime Date { get; set; }

        public BudgetRecord(string name, string type, decimal amount, bool isIncome, DateTime date)
        {
            Name = name;
            Type = type;
            Amount = amount;
            IsIncome = isIncome;
            Date = date;
        }
    }
}
