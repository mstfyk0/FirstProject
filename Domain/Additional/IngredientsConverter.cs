using Domain.Common;

namespace Domain.Additional
{   
    //class used to convert list of ingredients to string and string to list of ingredients
    public static class IngredientsConverterListToString
    {
        public static string listToString(List<Ingredient>? ingredientList)
        {
            string ingredients = "";
            if (ingredientList == null)
            {
                return ingredients;
            }
            foreach (var ingredient in ingredientList)
            {
                ingredients += ingredient.Name + "=" + ingredient.Weight + ",";
            }
            return ingredients.Remove(ingredients.Length - 1);

        }

        public static List<Ingredient> stringToList(string ingredients)
        {
            List<Ingredient> ingredientsList = new List<Ingredient>();
            if (ingredients == null)
            {
                return ingredientsList;
            }
            string[] ingredientsListString =ingredients.Split(",".ToCharArray()); 

            foreach (var ingredient in ingredientsListString)
            {
                string[] divideIngredient = ingredients.Split("=".ToCharArray());
                ingredientsList.Add(new Ingredient
                {
                    Name = divideIngredient[0],
                    Weight = Int32.Parse(divideIngredient[1])

                });
            }
            return ingredientsList;

        }
    }
}
