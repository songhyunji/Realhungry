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

    public Image comboImg;
    public Sprite combo1spr;
    public Sprite combo2spr;

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
        comboImg.gameObject.SetActive(false);
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
            switch(count)
            {
                case 1:
                    comboImg.gameObject.SetActive(true);
                    comboImg.sprite = combo1spr;
                    StopCoroutine("HideCombo");
                    StartCoroutine("HideCombo");
                    break;
                case 2:
                    comboImg.gameObject.SetActive(true);
                    comboImg.sprite = combo2spr;
                    StopCoroutine("HideCombo");
                    StartCoroutine("HideCombo");
                    break;
                case 3:
                    onFeverStart();
                    break;
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
        count = 0;

        StartCoroutine(FeverCount());
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

    IEnumerator HideCombo()
    {
        yield return new WaitForSeconds(0.5f);
        comboImg.gameObject.SetActive(false);
    }
}
