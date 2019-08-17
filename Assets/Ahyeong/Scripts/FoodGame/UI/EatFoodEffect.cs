using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EatFoodEffect : MonoBehaviour
{
    public SpriteRenderer sr;
    public Text scoreText;
    private int _score = 0;

    private void Start()
    {
        sr.sprite = null;
        scoreText.text = string.Empty;
    }

    public void ShowUI(Sprite foodSprite, int score)
    {
        sr.sprite = foodSprite;
        _score = score;
        StartCoroutine(PlayEffect());
    }

    IEnumerator PlayEffect()
    {
        // 작아짐
        float fromSize = 0.2f;
        float toSize = 0f;
        float step = Time.deltaTime / 1.5f;
        float t = 0f;
        while(t < 1f)
        {
            float size = Mathf.Lerp(fromSize, toSize, t);
            transform.localScale = new Vector3(size, size, size);
            transform.Rotate(new Vector3(0, 0, 10f));
            t += step;
            yield return null;
        }
        
        //transform.localScale = new Vector3(fromSize, fromSize, fromSize);
        sr.sprite = null;
        
        scoreText.text = "+" + _score.ToString();
        yield return new WaitForSeconds(1.0f);
        scoreText.text = string.Empty;
    }
}
