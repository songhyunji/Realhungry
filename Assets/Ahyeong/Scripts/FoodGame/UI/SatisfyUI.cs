using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SatisfyUI : MonoBehaviour
{
    public Image barImage;
    public Color normalModeColor = Color.white;
    public Color satisfyModeColor = Color.white;

    public void SetUI(float value)
    {
        barImage.fillAmount = value;
    }

    public void SetColorWithMode(bool isSatisfy)
    {
        barImage.color = isSatisfy ? satisfyModeColor : normalModeColor;
    }
}
