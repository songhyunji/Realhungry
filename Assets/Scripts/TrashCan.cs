using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag.CompareTo("Ingredient") == 0)
        {
            var obj = c.GetComponent<IngredientObject>();
            obj.DeactivateObject();
        }
    }
}
