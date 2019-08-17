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

    private List<FoodIngredient> ingredients = new List<FoodIngredient>();
    private Queue<TestIngredientUI> uiQueue = new Queue<TestIngredientUI>();

	private int count = 0;
	public GameObject effect;

	void Start()
    {
        recipeDatabase.Init();
        ingDB.Init();
        spawner.Init();
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

            FoodGameManager.Instance.Score += result.Score;
            FoodGameManager.Instance.AddSatisfy(result.Score);

			count++;
			if (count == 3)
			{
				Instantiate(effect, new Vector3(0, 0, 0), Quaternion.identity);
				count = 0;
			}
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

			count = 0;
        }

        while (uiQueue.Count > 0)
        {
            var ui = uiQueue.Dequeue();
            Destroy(ui.gameObject);
        }
        ingredients.Clear();

    }
}
