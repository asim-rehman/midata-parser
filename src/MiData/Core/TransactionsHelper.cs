using System;
using System.Text.RegularExpressions;

namespace MiData.Core
{
    public static class TransactionsHelper
    {
        public static DateTime ConvertStringToDate(this string date)
        {
            bool validDate = DateTime.TryParse(date, out DateTime newDate);

            if (validDate)
                return newDate;
            else
                return DateTime.MinValue;
        }

        public static Decimal ConvertToDecimal(this string amount)
        {
            string pattern = "[^-0-9.]";
            amount = Regex.Replace(amount, pattern, string.Empty, RegexOptions.None);

            if (Decimal.TryParse(amount, out Decimal newAmount))
                return newAmount;
            return 0;
        }

        public static string Clean(this string value)
        {
            if(value != null)
            {
                return value.Replace("\"", string.Empty);
            }
            return string.Empty;
        }
        public static int GetPercentage(int index, int total)
        {
            int value = index * 100;

            if (value != 0 && total != 0)
                return value / total;
            else
                return 0;
        }
    }
}