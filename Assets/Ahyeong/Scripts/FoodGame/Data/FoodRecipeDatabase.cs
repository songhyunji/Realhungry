using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FoodRecipeDatabase : ScriptableObject
{
    public List<FoodRecipe> recipes;
    public Dictionary<int, FoodRecipe> recipeDictionary = new Dictionary<int, FoodRecipe>();

    public void Init()
    {
        recipeDictionary.Clear();
        for(int i = 0; i < recipes.Count; i++)
        {
            recipes[i].Init();
            recipeDictionary.Add(recipes[i].RecipeKey, recipes[i]);
        }
    }

    public FoodRecipe MakeRecipe(List<FoodIngredient> ingredients)
    {
        ingredients.Sort();

        int key = 0;
        for(int i = 0; i< ingredients.Count; i++)
        {
            key *= 10;
            key += ingredients[i].id;
        }

        FoodRecipe recipe = null;
        if(recipeDictionary.ContainsKey(key))
        {
            recipe = recipeDictionary[key];
        }
        return recipe;
    }
}
