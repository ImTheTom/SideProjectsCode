using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This class was going to be used to manipulate dates, but it turns out the date time
/// object allready had the functions I was going to use
/// </summary>
namespace SQL_Lib {
    public static class DateManipulation {
        public static string ChangeDateToString(DateTime today) {
            string date = today.ToString();
            StringBuilder actualDate = new StringBuilder();
            actualDate.Append(date[6]);
            actualDate.Append(date[7]);
            actualDate.Append(date[8]);
            actualDate.Append(date[9]);
            actualDate.Append('-');
            actualDate.Append(date[3]);
            actualDate.Append(date[4]);
            actualDate.Append('-');
            actualDate.Append(date[0]);
            actualDate.Append(date[1]);
            return actualDate.ToString();
        }
    }
}
