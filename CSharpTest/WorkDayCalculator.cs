using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {

        public int WeekEndDays(DateTime start, DateTime end, WeekEnd[] weekEnds)
        {
            int i = IndexOfNextWeekEnd(start, weekEnds);

            if (i == -1 || weekEnds == null) // there are no weekends anymore
                return 0;

            if (DateTime.Compare(weekEnds[i].StartDate, end) > 0) // the remained weekends are not relevant
                return 0;

            DateTime nextStart = (weekEnds[i].EndDate).AddDays(1);
            int daysInCurrentWeekend = (weekEnds[i].EndDate - weekEnds[i].StartDate).Days + 1;
            return daysInCurrentWeekend + WeekEndDays(nextStart, end, weekEnds);
        }

        public int IndexOfNextWeekEnd(DateTime start, WeekEnd[] weekEnds)
        {
            if(start == null || weekEnds == null)
                return 0;
            int indexOfFirstWeekend = 0; // initialize with the first weekend
            foreach (WeekEnd weekend in weekEnds)
            {
                if (DateTime.Compare(weekend.StartDate, start) > 0)
                    return indexOfFirstWeekend;
                else
                    indexOfFirstWeekend++;
            }
            return -1; // there are no weekends anymore
        }
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            try
            {
                DateTime result = startDate;

                // add the number of days
                result = result.AddDays(dayCount - 1);

                // if there are no weekends, we don't have to compute anything else
                if (weekEnds == null) 
                    return result;

                // subtract the weekends
                int numberOfWeekendDays = WeekEndDays(startDate, result, weekEnds);

                result = result.AddDays(numberOfWeekendDays);

                Console.WriteLine(result);
                return result;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }    
    }
}
