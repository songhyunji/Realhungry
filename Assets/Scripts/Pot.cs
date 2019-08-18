using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public ParticleSystem effect;

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Ingredient")
        {
            var obj = c.GetComponent<IngredientObject>();

            if (obj.data.id >= 5)
            {
                effect.gameObject.SetActive(true);
                effect.Play();
                TestFoodMaker.Instance.EndGame();
            }

            obj.AddIngredient();
            obj.DeactivateObject();
        }
    }

    public void MakeRecipe()
    {
        TestFoodMaker.Instance.MakeRecipe();
    }
}
