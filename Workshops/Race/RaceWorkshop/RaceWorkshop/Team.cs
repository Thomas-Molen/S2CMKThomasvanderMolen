using System;
using System.Collections.Generic;
using System.Text;

namespace RaceWorkshop
{
    class Team
    {
        private string name;
        private string city;
        private string country;
        private string mainSponsor;
        private int foundingYear;
        private string director;

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
