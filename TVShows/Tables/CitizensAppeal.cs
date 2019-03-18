using System;
using System.Collections.Generic;
using System.Text;

namespace TVShows.Tables
{
    public class CitizensAppeal
    {
        public int CitizensAppealID { get; set; }
        public string LFO { get; set; }
        public string Organization { get; set; }
        public string GoalOfRequest { get; set; }
        public int ScheduleForWeekID { get; set; }

        public virtual ScheduleForWeek ScheduleForWeek { get; set; }
    }
}
