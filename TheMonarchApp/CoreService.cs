using Newtonsoft.Json;
using System.Configuration;
using System.Data;

namespace TheMonarchApp
{
    public class CoreService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = ConfigurationManager.AppSettings["dataFetchUrl"];
        public CoreService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public List<MonarchDataModel> GenerateDataFromOnlineJson()
        {
            var response = _httpClient.GetAsync(_url).Result;
            response.EnsureSuccessStatusCode();
            var json = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<MonarchDataModel>>(json);
        }

        public MonarchViewModel GetDataToDisplay(List<MonarchDataModel> monarchSet)
        {
            MonarchViewModel dataObject = new MonarchViewModel();

            //Question: 1
            //Total Monarch Count
            dataObject.TotalMonarchsCount = monarchSet.Count;

            //Question: 2
            //Monarch ruled the longest with numbers of years
            dataObject.MonarchDataModel = monarchSet.OrderByDescending(m => m.EndYear - m.StartYear).First();
            dataObject.LongestYears = dataObject.MonarchDataModel.EndYear - dataObject.MonarchDataModel.StartYear;

            //Question: 3
            //House ruled the longest with numbers of years
            var house = monarchSet.GroupBy(m => m.House)
                                      .ToDictionary(g => g.Key, g => g.Sum(m => m.EndYear - m.StartYear));

            dataObject.LongestHouse = house.OrderByDescending(h => h.Value).First();
            dataObject.LongestHouseYears = dataObject.LongestHouse.Value;

            //Question: 4
            //Most common first name in the dataset
            var firstNames = monarchSet.Select(m => m.Name.Split()[0]);
            dataObject.CommonFirstName = firstNames.GroupBy(n => n)
                                                .OrderByDescending(g => g.Count())
                                                .First()
                                                .Key;

            //Question: 5
            //House of the current monarch and for number of years house ruled throughout history
            int currentYear = DateTime.Now.Year;
            dataObject.MonarchDataModel = monarchSet.OrderByDescending(m => m.EndYear <= currentYear ? m.EndYear : 0).First();
            dataObject.CurrentMonarchHouseYears = house[dataObject.MonarchDataModel.House];
            return dataObject;
        }
    }
}
