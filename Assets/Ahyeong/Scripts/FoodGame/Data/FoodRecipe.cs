using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FoodRecipe : ScriptableObject
{
    public int RecipeKey
    {
        get
        {
            int key = 0;
            for(int i = 0; i < ingredients.Count; i++)
            {
                key *= 10;
                key += ingredients[i].id;
            }
            Debug.Log(key);
            return key;
        }
    }

    public int RecipeValue
    {
        get { return ingredients.Count; }
    }

    // 레시피 이름, 그림, 설명, 필요 재료, 계수
    public string nameStr;
    [TextArea]
    public string description;
    public List<FoodIngredient> ingredients;
    public Sprite sprite;

    public void Init()
    {
        ingredients.Sort();
    }
}
