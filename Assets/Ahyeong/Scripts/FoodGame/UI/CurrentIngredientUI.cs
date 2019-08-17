using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentIngredientUI : MonoBehaviour
{
    public List<Image> imageSlots;
    public List<Text> scoreTexts;
    public Sprite defaultSprite;
    public List<int> scores;
    private bool isPlaying = false;
    private List<FoodIngredient> _ingrdients = new List<FoodIngredient>();

    private void Start()
    {
        ResetUI();

        foreach (Text txt in scoreTexts)
        {
            txt.text = string.Empty;
        }
    }

    public void ResetUI()
    {
        _ingrdients.Clear();

        foreach (Image img in imageSlots)
        {
            img.sprite = defaultSprite;
        }
    }

    public void AddUI(FoodIngredient ingredient)
    {
        if (isPlaying)
        {
            StopAllCoroutines();
            scores.Clear();
            isPlaying = false;
        }

        imageSlots[_ingrdients.Count].sprite = ingredient.sprite;
        _ingrdients.Add(ingredient);
        scores.Add(ingredient.score);
    }

    public void SetUI(List<FoodIngredient> ingredients)
    {
        if(isPlaying)
        {
            StopAllCoroutines();
            scores.Clear();
        }

        for(int i = 0; i < ingredients.Count; i++)
        {
            imageSlots[i].sprite = ingredients[i].sprite;
            scores.Add(ingredients[i].score);
        }
    }

    public void ShowUI()
    {
        StartCoroutine(PlayEffect());
    }

    IEnumerator PlayEffect()
    {
        isPlaying = true;
        Debug.Log("?");
        for (int i = 0; i < scores.Count; i++)
        {
            scoreTexts[i].text = "+" + scores[i].ToString();
        }

        scores.Clear();

        yield return new WaitForSeconds(1.0f);

        Debug.Log("??");
        for (int i = 0; i < scoreTexts.Count; i++)
        {
            scoreTexts[i].text = string.Empty;
        }
        isPlaying = false;
    }
}
