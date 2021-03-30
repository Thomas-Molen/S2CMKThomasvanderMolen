using System;
using System.Collections.Generic;
using System.Text;

namespace RaceWorkshop2
{
    public class Team
    {
        public string name { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string mainSponsor { get; set; }
        public int foundingYear { get; set; }
        public string director { get; set; }

        public void UpdateTeam(string Name, string City, string Country, string MainSponsor, int FoundingYear, string Director)
        {
            name = Name;
            city = City;
            country = Country;
            mainSponsor = MainSponsor;
            foundingYear = FoundingYear;
            director = Director;
        }
    }
}
