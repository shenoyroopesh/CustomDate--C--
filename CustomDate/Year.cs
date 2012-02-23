using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomDate
{
    public class Year
    {
        public List<Month> Months { get; set; }

        public int AD { get; private set; }

        public bool Leap 
        {
            get
            {
                if (this.AD % 400 == 0)
                    return true;

                if (this.AD % 4 == 0 && this.AD % 100 != 0)
                    return true;
                
                return false;
            }
        }


        public Year(int AD)
        {
            this.AD = AD;

            this.Months = new List<Month>();
            for (int i = 1; i <= 12; i++)
            {
                this.Months.Add(new Month((MonthType)i, this));
            }
        }

        public Month GetMonth(string monthName)
        {
            return this.Months
                    .Where(p => p.MonthType == 
                                (MonthType)Enum.Parse(typeof(MonthType), monthName))
                    .First();
        }

        public Month GetMonth(int monthIndex)
        {
            return this.Months.Where(p => (int)p.MonthType == monthIndex).First();
        }

        public int DaysCount
        {
            get
            {
                return this.Leap ? 366 : 365;
            }
        }
    }
}