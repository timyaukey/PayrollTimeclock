using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollTimeclock
{
    public class PayrollPeriod
    {
        public readonly DateTime StartDate;
        public readonly DateTime EndDate;

        public PayrollPeriod(DateTime startDate)
        {
            StartDate = startDate;
            EndDate = startDate.AddDays(PayrollStatic.Settings.DaysInPeriod - 1.0);
        }

        public bool ContainsDate(DateTime when)
        {
            return when.Date >= StartDate.Date &&
                when.Date <= EndDate.Date;
        }

        public override string ToString()
        {
            return EndDate.ToShortDateString();
        }
    }
}
