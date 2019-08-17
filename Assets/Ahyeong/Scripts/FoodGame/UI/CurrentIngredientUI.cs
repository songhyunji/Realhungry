using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentIngredientUI : MonoBehaviour
{
    public List<Image> imageSlots;
    public Sprite defaultSprite;

    private void Start()
    {
        ResetUI();
    }

    public void ResetUI()
    {
        foreach (Image img in imageSlots)
        {
            img.sprite = defaultSprite;
        }
    }

    public void SetUI(List<FoodIngredient> ingredients)
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            imageSlots[i].sprite = ingredients[i].sprite;
        }
    }
}
