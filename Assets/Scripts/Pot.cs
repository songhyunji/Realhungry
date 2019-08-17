using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Ingredient")
        {
            var obj = c.GetComponent<IngredientObject>();
            obj.AddIngredient();
            obj.DeactivateObject();
        }
    }

    public void MakeRecipe()
    {
        TestFoodMaker.Instance.MakeRecipe();
    }
}
