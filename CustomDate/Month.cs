using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomDate
{
    public class Month
    {
        public MonthType MonthType { get; private set; }

        public Year Year { get; private set; }

        public Month(MonthType monthType, Year year)
        {
            this.MonthType = monthType;
            this.Year = year;
        }

        public int GetDaysCount()
        {
            if ((new List<MonthType>() { MonthType.January, MonthType.March, MonthType.May, MonthType.July, MonthType.August, MonthType.October, MonthType.December })
                .Contains(this.MonthType))
                return 31;

            if (this.MonthType == MonthType.February)
            {
                return this.Year.Leap ? 29 : 28;
            }

            return 30;
        }
    }
}