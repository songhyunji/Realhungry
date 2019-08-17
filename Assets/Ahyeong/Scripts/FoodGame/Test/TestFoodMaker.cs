using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestFoodMaker : MonoSingleton<TestFoodMaker>
{
    public FoodRecipeDatabase recipeDatabase;
    public FoodIngredientDatabase ingDB;
    public TestIngredientUI prefab;
    public Transform uiParent;
    public Image resultImg;
    public Text resultText;
    public Spawner spawner;

    public CurrentIngredientUI ingredientUI;

    private List<FoodIngredient> ingredients = new List<FoodIngredient>();
    //private Queue<TestIngredientUI> uiQueue = new Queue<TestIngredientUI>();

    public void Init()
    {
        recipeDatabase.Init();
        ingDB.Init();
    }

    public void ResetValue()
    {
        ingredients.Clear();
        ingredientUI.ResetUI();
    }

    public void AddIngredient(FoodIngredient ingredient)
    {
        // 쓰레기 체크
        if(ingredient.id >= 5)
        {
            FoodGameManager.Instance.Score += ingredient.score;
            ingredients.Clear();
            ingredientUI.ResetUI();
            return;
        }

        ingredients.Add(ingredient);
        ingredientUI.SetUI(ingredients);

        // TO-DO: 연출 필요
        if(ingredients.Count >= 4)
        {
            MakeRecipe();
        }
    }

    public void MakeRecipe()
    {
        FoodRecipe result = recipeDatabase.MakeRecipe(ingredients);
        if(result)
        {
            resultImg.sprite = result.sprite;
            resultText.text = result.nameStr;

            FoodGameManager.Instance.Score += result.Score;
            FoodGameManager.Instance.AddSatisfy(result.Score);
        }
        else
        {
            resultImg.sprite = null;
            resultText.text = string.Empty;

            int sum = 0;
            foreach(var ing in ingredients)
            {
                sum += ing.score;
            }
            FoodGameManager.Instance.Score += sum;
            FoodGameManager.Instance.AddSatisfy(sum);
        }

        ingredients.Clear();
        ingredientUI.ResetUI();
    }
}
