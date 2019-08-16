using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientObject : MonoBehaviour
{
    public SpriteRenderer spRenderer;
    private FoodIngredient data;

    public Rigidbody2D rigid;

    public void Init(FoodIngredient ing)
    {
        data = ing;
        spRenderer.sprite = data.sprite;
        rigid.sharedMaterial.bounciness = ing.weight;
        
    }

    public void AddIngredient()
    {
        TestFoodMaker.Instance.AddIngredient(this.data);
    }
}
