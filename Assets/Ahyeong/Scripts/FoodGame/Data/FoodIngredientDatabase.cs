using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 주목!!
// 원래 시스템으로 가는게 맞으나 쓰레기 시스템이 추가되어
// 개발 기간 단축을 위해 같은 DB를 쓰도록함
// 쓰레기 판별법은 id>4로 하드코딩으로 판별

// 추후 리팩토링 시에는 재료와 쓰레기의 공통 부모 클래스를 만들어 공통 내용을 구현하고
// 자식클래스에서 각각의 내용을 구현하도록 하는 것을 생각.

// 백업용 코드는 주석처리

[CreateAssetMenu]
public class FoodIngredientDatabase : ScriptableObject
{
    public List<FoodIngredient> ingredients = new List<FoodIngredient>();
    
    private List<int> _ingredientProbabilityTable = new List<int>();
    private List<int> _trashProbabilityTable = new List<int>();
    private int _ingredientProbabilityMax;
    private int _trashProbabilityMax;

    public void Init()
    {
        _ingredientProbabilityTable.Clear();
        int sum = 0;
        for(int i = 0; i < 4; i++)
        {
            sum += ingredients[i].probability;
            _ingredientProbabilityTable.Add(sum);
        }
        _ingredientProbabilityMax = sum;

        sum = 0;
        for (int i = 4; i < ingredients.Count; i++)
        {
            sum += ingredients[i].probability;
            _trashProbabilityTable.Add(sum);
        }
        _trashProbabilityMax = sum;
    }

    public void Clear()
    {
        ingredients.Clear();
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
        int rand = Random.Range(0, _ingredientProbabilityMax);

        for (int i = 0; i < _ingredientProbabilityTable.Count; i++)
        {
            if(rand < _ingredientProbabilityTable[i])
            {
                return ingredients[i];
            }
        }

        return null;
    }

    public FoodIngredient GetRandomTrash()
    {
        int rand = Random.Range(0, _trashProbabilityMax);

        for (int i = 0; i < _trashProbabilityTable.Count; i++)
        {
            if (rand < _trashProbabilityTable[i])
            {
                return ingredients[i + 4];
            }
        }

        return null;
    }
}

/*
 * [CreateAssetMenu]
public class FoodIngredientDatabase : ScriptableObject
{
    public List<FoodIngredient> ingredients = new List<FoodIngredient>();
    
    private List<int> _probabilityTable = new List<int>();
    private int _probabilityMax;

    public void Init()
    {
        _probabilityTable.Clear();
        int sum = 0;
        for(int i = 0; i < ingredients.Count; i++)
        {
            sum += ingredients[i].probability;
            _probabilityTable.Add(sum);
        }
        _probabilityMax = sum;
    }

    public void Clear()
    {
        ingredients.Clear();
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

 */
