using Microsoft.Extensions.DependencyInjection;

namespace TheMonarchApp
{
    internal class Program
    {
        public static void Main()
        {
            //Create a DI container
            var serviceProvider = new ServiceCollection()
                .AddTransient<CoreService>()
                .AddTransient<HttpClient>()
                .BuildServiceProvider();

            //Resolve Dependency
            var serviceObj = serviceProvider.GetRequiredService<CoreService>();

            //Get Online Data
            List<MonarchDataModel> monarchSet = serviceObj.GenerateDataFromOnlineJson();

            if (monarchSet != null)
            {
                MonarchViewModel objViewModel = serviceObj.GetDataToDisplay(monarchSet);

                Console.WriteLine("1. How many monarchs are in the dataset? \n" + objViewModel.TotalMonarchsCount);
                Console.WriteLine("2. Which monarch ruled the longest and for how many years? \n" + objViewModel.MonarchDataModel.Name + " for " + objViewModel.LongestYears + " years");
                Console.WriteLine("3. Which house ruled the longest and for how many years? \n" + objViewModel.LongestHouse.Key + " for " + objViewModel.LongestHouseYears + " years");
                Console.WriteLine("4. What is the most common first name in the dataset? \n" + objViewModel.CommonFirstName);
                Console.WriteLine("5. What is the house of the current monarch and for how many years did that house rule throughout history? \n" +
                      objViewModel.MonarchDataModel.House + " for " + objViewModel.CurrentMonarchHouseYears + " years");
                Console.ReadLine();

            }
        }
    }
}