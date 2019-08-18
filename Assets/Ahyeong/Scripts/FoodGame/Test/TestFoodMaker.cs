using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnFeverStart();
public delegate void OnFeverEnd();

public class TestFoodMaker : MonoSingleton<TestFoodMaker>
{
    public FoodRecipeDatabase recipeDatabase;
    public FoodIngredientDatabase ingDB;
    public TestIngredientUI prefab;
    public Transform uiParent;
    //public Image resultImg;
    public EatFoodEffect foodEffect;
    //public Text resultText;
    public Spawner spawner;

    public CurrentIngredientUI ingredientUI;

    private List<FoodIngredient> ingredients = new List<FoodIngredient>();
    private List<int> scores = new List<int>();
	//private Queue<TestIngredientUI> uiQueue = new Queue<TestIngredientUI>();

	public bool fevertime;
	public GameObject effector;
	[SerializeField]
	private int count;

    public event OnFeverStart onFeverStart;
    public event OnFeverEnd onFeverEnd;

    public void Init()
    {
        recipeDatabase.Init();
        ingDB.Init();

        onFeverStart += new OnFeverStart(FeverStart);
        onFeverEnd += new OnFeverEnd(FeverEnd);
    }

    public void ResetValue()
    {
        ingredients.Clear();
        ingredientUI.ResetUI();
        count = 0;
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
        if(ingredients.Count == 0)
        {
            return;
        }

        FoodRecipe result = recipeDatabase.MakeRecipe(ingredients);
        if(result)
        {
            SoundManager.Instance.Play(1);

            foodEffect.ShowUI(result.sprite, result.Score);

			if(!fevertime)	// 피버타임이 아닐 때만 count
			{
				count++;
				FoodGameManager.Instance.Score += result.Score;
				FoodGameManager.Instance.AddSatisfy(result.Score);
			}
			else
			{
				FoodGameManager.Instance.Score += result.Score * 2;
				FoodGameManager.Instance.AddSatisfy(result.Score * 2);
			}

			if(count == 3)	// 3 콤보 달성 시
			{
                onFeverStart();
			}

        }
        else
        {
			count = 0;	// 콤보 연속 달성 실패 시 count 초기화

            scores.Clear();
            int sum = 0;
            foreach(var ing in ingredients)
            {
                sum += ing.score;
                scores.Add(ing.score);
            }
            FoodGameManager.Instance.Score += sum;
            FoodGameManager.Instance.AddSatisfy(sum);
        }

        ingredients.Clear();
        ingredientUI.ResetUI();
    }

    GameObject FeverEffect;

    void FeverStart()
    {
        SoundManager.Instance.Play(2);

        fevertime = true;
        FeverEffect = Instantiate(effector, new Vector3(0, 0, 0), Quaternion.identity);

        StartCoroutine(FeverCount());
    }

    public void EndGame()
    {
        StopAllCoroutines();
        onFeverEnd();
        count = 0;
    }

    void FeverEnd()
    {
        fevertime = false;
        Destroy(FeverEffect);
    }

    IEnumerator FeverCount()
    {
        yield return new WaitForSeconds(10.5f);

        onFeverEnd();
    }
}
