
namespace Domain.Additional
{
    public class NutritionRange
    {
        public uint MinCalories { get; set; }
        public uint MaxCalories { get; set; }
        public uint MinProtein { get; set; }
        public uint MaxProtein { get; set; }
        public uint MinCarbonHydrates { get; set; }
        public uint MaxCarbonHydrates { get; set; }
        public uint MinFat { get; set; }
        public uint MaxFat { get; set; }
        public string Name { get; set; }

        public NutritionRange(uint minCalories,
                               uint maxCalories,
                               uint minProtein, uint maxProtein, uint minCarbonHydrates, uint maxCarbonHydrates, uint minFat, uint maxFat, string name) 
        { 
            MinCalories = minCalories;
            MaxCalories = maxCalories;
            MinProtein = minProtein;
            MaxProtein = maxProtein;
            MinCarbonHydrates = minCarbonHydrates;
            MaxCarbonHydrates = maxCarbonHydrates;
            MinFat= minFat;
            MaxFat= maxFat;
            Name = name;
        }
    }                                                                                                           
                                                                                                                
}
