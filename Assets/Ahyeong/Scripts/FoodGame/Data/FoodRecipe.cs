using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[System.Serializable]
public class RecipeEntry: IEquatable<RecipeEntry>, IComparable<RecipeEntry>
{
    public int Score
    {
        get { return ingredient.score * count; }
    }

    public FoodIngredient ingredient;
    public int count = 1;

    public RecipeEntry(FoodIngredient ing, int cunt)
    {
        ingredient = ing;
        count = cunt;
    }

    public bool Equals(RecipeEntry other)
    {
        if (other == null) return false;
        return (this.ingredient.id.Equals(other.ingredient.id));
    }

    public int CompareTo(RecipeEntry comparePart)
    {
        if (comparePart == null)
            return 1;

        else
            return this.ingredient.id.CompareTo(comparePart.ingredient.id);
    }
}

[CreateAssetMenu]
public class FoodRecipe : ScriptableObject
{
    public int RecipeKey
    {
        get
        {
            if(_recipeKey < 0)
            {
                int key = 0;
                for (int i = 0; i < ingrediensEntries.Count; i++)
                {
                    for (int j = 0; j < ingrediensEntries[i].count; j++)
                    {
                        key *= 10;
                        key += ingrediensEntries[i].ingredient.id;
                    }
                }
                Debug.Log(key);
                _recipeKey = key;
            }
            return _recipeKey;
        }
    }

    public int Score
    {
        get { return _scoreSum * value; }
    }

    // 레시피 이름, 그림, 설명, 필요 재료, 계수
    public string nameStr;
    [TextArea]
    public string description;
    public int value;
    public List<RecipeEntry> ingrediensEntries = new List<RecipeEntry>();
    public Sprite sprite;
    private int _recipeKey = -1;
    private int _scoreSum = 0;

    public void Init()
    {
        ingrediensEntries.Sort();

        _scoreSum = 0;
        for(int i = 0; i< ingrediensEntries.Count; i++)
        {
            _scoreSum += ingrediensEntries[i].Score;
        }
    }


#if UNITY_EDITOR
    public void SetData(Dictionary<string, object> data, FoodIngredientDatabase ingredientDatabase)
    {
        // name	description
        nameStr = (string)data["name"];
        description = (string)data["description"];
        value = (int)data["value"];

        for (int i = 1; i <= ingredientDatabase.ingredients.Count; i++)
        {
            string key = "ing_" + i;
            if(data.ContainsKey(key) && data[key] != null)
            {
                int count;
                try
                {
                    count = (int)data[key];
                }
                catch
                {
                    continue;
                }

                var ingredient = ingredientDatabase.GetIngredient(i);
                ingrediensEntries.Add(new RecipeEntry(ingredient, count));
            }
        }
    }
#endif
}
