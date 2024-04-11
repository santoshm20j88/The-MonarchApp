using Newtonsoft.Json;
using System;

namespace TheMonarchApp
{
    public class MonarchDataModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nm")]
        public string Name { get; set; }

        [JsonProperty("cty")]
        public string Country { get; set; }

        [JsonProperty("hse")]
        public string House { get; set; }

        [JsonIgnore]
        public string Years { get; set; }

        // Extra properties added to calculate the tenure 
        public int StartYear { get; private set; }
        public int EndYear { get; private set; }

        /// <summary>
        /// This property is used to calculate the Start and End Year from the given value in json object
        /// </summary>
        [JsonProperty("yrs")]
        private string SerialYears
        {
            get => Years;
            set
            {
                // Split the "years" field into start and end years
                string[] parts = value.Split('-');
                if (parts.Length == 1 && int.TryParse(parts[0], out int startYearLen1) && int.TryParse(parts[0], out int endYearLen1))
                {
                    StartYear = startYearLen1;
                    EndYear = endYearLen1;
                }
                else if (parts.Length == 2)
                {
                    StartYear = int.TryParse(parts[0], out int startYearLen2) ? startYearLen2 : 0;
                    //For last data Elizabeth II year has been given as "1952-" so
                    //Considering it from 1952 to present
                    if (StartYear == 1952)
                    {
                        EndYear = DateTime.Now.Year;
                    }
                    else if (int.TryParse(parts[1], out int endYearLen2))
                    {
                        EndYear = endYearLen2;
                    }
                }
                else
                {
                    throw new JsonSerializationException("Invalid years.");
                }
            }
        }
    }
}
