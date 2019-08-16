using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestFoodMaker : MonoSingleton<TestFoodMaker>
{
    public FoodRecipeDatabase recipeDatabase;
    public TestIngredientUI prefab;
    public Transform uiParent;
    public Image resultImg;
    public Text resultText;

    private List<FoodIngredient> ingredients = new List<FoodIngredient>();
    private Queue<TestIngredientUI> uiQueue = new Queue<TestIngredientUI>();

    void Start()
    {
        recipeDatabase.Init();
    }

    public void AddIngredient(FoodIngredient ingredient)
    {
        var ui = Instantiate(prefab, uiParent);
        ui.SetUI(ingredient);
        uiQueue.Enqueue(ui);
        ingredients.Add(ingredient);
    }

    public void MakeRecipe()
    {
        FoodRecipe result = recipeDatabase.MakeRecipe(ingredients);
        if(result)
        {
            resultImg.sprite = result.sprite;
            resultText.text = result.nameStr;
        }
        else
        {
            resultImg.sprite = null;
            resultText.text = string.Empty;
        }

        while (uiQueue.Count > 0)
        {
            var ui = uiQueue.Dequeue();
            Destroy(ui.gameObject);
        }
        ingredients.Clear();

    }
}
