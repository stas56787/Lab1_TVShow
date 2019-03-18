using System;
using System.Collections.Generic;
using System.Text;

namespace TVShows.Tables
{
    public class TVShow
    {
        public int TVShowID { get; set; }
        public string NameShow { get; set; }
        public string Duration { get; set; }
        public string Rating { get; set; }
        public string DescriptionShow { get; set; }
        public int GenreID { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual ICollection<ScheduleForWeek> SchedulesForWeeks { get; set; }
    }
}
