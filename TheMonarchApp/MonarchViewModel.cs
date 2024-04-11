using System.Collections.Generic;

namespace TheMonarchApp
{
    public  class MonarchViewModel
    {
        public int TotalMonarchsCount { get; set; }

        public int LongestYears { get; set; }

        public int LongestHouseYears { get; set; }

        public string CommonFirstName { get; set; }

        public int CurrentMonarchHouseYears { get; set; }

        public string CurrentMonarchHouse { get; set; }

        public KeyValuePair<string, int> LongestHouse { get; set; }

        public MonarchDataModel MonarchDataModel { get; set; }
    }
}
