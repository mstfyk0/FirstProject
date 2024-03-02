using Microsoft.AspNetCore.Mvc;
using Domain.Model_Binder;

namespace Domain.Common
{
    [ModelBinder(BinderType=typeof (MetaDataValueModelBinder))]
    public class Ingredient
    {
        public string Name { get; set; }
        public int Weight { get; set; }
    }   
}
