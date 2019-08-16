using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestIngredientUI : MonoBehaviour
{
    public FoodIngredient data;
    public Image img;
    public Text txt;

    private void Start()
    {
        if(data)
        {
            img.sprite = data.sprite;
            txt.text = data.nameStr;
        }
    }

    public void SetUI(FoodIngredient newData)
    {
        data = newData;
        img.sprite = data.sprite;
        txt.text = data.nameStr;
    }

    public void AddIngredient()
    {
        TestFoodMaker.Instance.AddIngredient(this.data);
    }
}
