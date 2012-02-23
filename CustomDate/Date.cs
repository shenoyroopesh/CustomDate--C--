using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomDate
{
    public class Date
    {
        private string date;

        /// <summary>
        /// Day in the month
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// Year to which this date belongs
        /// </summary>
        public Year Year { get; set; }

        /// <summary>
        /// Month to which this date belongs
        /// </summary>
        public Month Month { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="date">Date to be instantiated in String format - (dd-mm-yyyy)</param>
        public Date(string date)
        {
            try
            {
                this.date = date;
                var splitted = date.Split('-');

                this.Year = new Year(Int32.Parse(splitted[2]));
                this.Month = this.Year.GetMonth(Int32.Parse(splitted[1]));
                this.Day = Int32.Parse(splitted[0]);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Please enter date in valid format - dd-mm-yyyy", "date");
            }
        }

        /// <summary>
        /// Gets the difference between this date and another date
        /// </summary>
        /// <param name="date">Date with which this date needs to be compared</param>
        /// <returns>Difference in integer</returns>
        public int GetDiff(Date date)
        {
            //different years
            if (this.Year.AD != date.Year.AD)
            {
                if (date.Year.AD < this.Year.AD)
                {
                    return date.GetDiff(this);
                }

                int dayCount = this.GetDaysRemainingInYear() + (date.Year.DaysCount - date.GetDaysRemainingInYear() + 1); //+1 to make it inclusive of to-date
                //add days in years in between

                for (int i = this.Year.AD + 1; i < date.Year.AD; i++)
                {
                    dayCount += (new Year(i)).DaysCount;
                }
                return dayCount;
            }

            if (this.Month.MonthType == date.Month.MonthType)
            {

                if (date.Day < this.Day)
                    return date.GetDiff(this);

                return date.Day - this.Day + 1;
            }
            else
            {
                if ((int)date.Month.MonthType < (int)this.Month.MonthType)
                {
                    return date.GetDiff(this);
                }

                return this.GetDaysRemainingInMonth() +
                       date.Day +
                    //days in in-between months
                       this.Year.Months.Where(p => (int)p.MonthType < (int)date.Month.MonthType &&
                                                 (int)p.MonthType > (int)this.Month.MonthType)
                                       .Sum(p => p.GetDaysCount());
            }
        }

        /// <summary>
        /// Gets the days remaining in this particular month from this date (including this date)
        /// </summary>
        /// <returns></returns>
        internal int GetDaysRemainingInMonth()
        {
            return this.Month.GetDaysCount() - this.Day + 1;
        }

        internal int GetDaysRemainingInYear()
        {
            return this.GetDaysRemainingInMonth() +
                //days in all remaining months
                   this.Year.Months.Where(p => (int)p.MonthType > (int)this.Month.MonthType)
                                   .Sum(p => p.GetDaysCount());
        }
    }
}