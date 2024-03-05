namespace SecondProject.Filters
{
    public class NutritionValuesFilter
    {
        public uint MinCalories { get; set; }
        public uint MaxCalories { get; set; } = 9999;

        public uint MinProtein { get; set; }
        public uint MaxProtein { get; set; } = 999;

        public uint MinCarbonhydrates { get; set; }
        public uint MaxCarbonhydrates { get; set; } = 999;
        public uint MinFat { get; set; }
        public uint MaxFat { get; set; } = 999;

        public string Name { get; set; } = "";

        public NutritionValuesFilter(uint minCalories , uint maxCalories, uint minProtein, uint maxProtein, uint minCarbonhydrates , uint maxCarbonhydrates, uint minFat, uint maxFat, string name)
        {
            MinCalories = minCalories;
            MaxCalories = maxCalories;
            MinProtein = minProtein;
            MaxProtein = maxProtein;
            MinCarbonhydrates = minCarbonhydrates;
            MaxCarbonhydrates = maxCarbonhydrates;
            MinFat = minFat;
            MaxFat = maxFat;
            Name = name;
        }
        public NutritionValuesFilter()
        {

        }
        public bool ValidFilterValues()
        {
            return (MaxCalories >= MinCalories && MaxProtein >= MinProtein && MaxCarbonhydrates >= MinCarbonhydrates && MaxFat >= MinFat);
        }



    }
}
