using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FoodIngredientDatabase : ScriptableObject
{
    public List<FoodIngredient> ingredients;
    
    private List<int> _probabilityTable = new List<int>();
    private int _probabilityMax;

    public void Init()
    {
        int sum = 0;
        for(int i = 0; i < ingredients.Count; i++)
        {
            sum += ingredients[i].probility;
            _probabilityTable.Add(sum);
        }
        _probabilityMax = sum;
    }

    public FoodIngredient GetIngredient(int id)
    {
        for(int i = 0; i< ingredients.Count; i++)
        {
            if(ingredients[i].id == id)
            {
                return ingredients[i];
            }
        }

        return null;
    }

    public FoodIngredient GetRandomIngredient()
    {
        int rand = Random.Range(0, _probabilityMax);

        for (int i = 0; i < _probabilityTable.Count; i++)
        {
            if(rand < _probabilityTable[i])
            {
                return ingredients[i];
            }
        }

        return null;
    }
}
