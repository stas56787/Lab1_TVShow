using System;
using System.Collections.Generic;
using System.Text;

namespace TVShows.Tables
{
    public class ScheduleForWeek
    {
        public int ScheduleForWeekID { get; set; }
        public string StartTime { get; set; }
        public string GuestsEmployees { get; set; }
        public int TVShowID { get; set; }

        public virtual TVShow TVShow { get; set; }
        public virtual ICollection<CitizensAppeal> CitizensAppeals { get; set; }
    }
}
