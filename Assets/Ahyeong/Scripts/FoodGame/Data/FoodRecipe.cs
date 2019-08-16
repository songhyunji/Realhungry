using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[System.Serializable]
public class RecipeEntry: IEquatable<RecipeEntry>, IComparable<RecipeEntry>
{
    public FoodIngredient ingredient;
    public int count = 1;

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

    public int RecipeValue
    {
        get { return ingrediensEntries.Count; }
    }

    // 레시피 이름, 그림, 설명, 필요 재료, 계수
    public string nameStr;
    [TextArea]
    public string description;
    public List<RecipeEntry> ingrediensEntries;
    public Sprite sprite;
    private int _recipeKey = -1;

    public void Init()
    {
        ingrediensEntries.Sort();
    }
}
