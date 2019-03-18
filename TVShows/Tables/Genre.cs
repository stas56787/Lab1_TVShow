using System;
using System.Collections.Generic;
using System.Text;

namespace TVShows.Tables
{
    public class Genre
    {
        public int GenreID { get; set; }
        public string NameGenre { get; set; }
        public string DescriptionOfGenre { get; set; }

        public virtual ICollection<TVShow> TVShows { get; set; }
    }
}
