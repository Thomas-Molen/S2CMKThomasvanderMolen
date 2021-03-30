using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceWorkshop2
{
    public class RaceDiscipline
    {
        List<Season> seasons;

        public string name { get; set; }
        public int since { get; set; }
        public bool isActive { get; set; }

        public RaceDiscipline()
        {
            seasons = new List<Season>();
        }

        public void AddSeason(Season season)
        {
            if (seasons.Any(s => s.year == season.year))
            {
                throw new ArgumentException(string.Format("Season with year {o} already exists in collection.", season.year));
            }

            seasons.Add(season);
        }

        public void RemoveSeason(Season season)
        {
            seasons.Remove(season);
        }

        public void UpdateRaceDiscipline(string Name, int Since, bool Active)
        {
            name = Name;
            since = Since;
            isActive = Active;
        }
    }
}
