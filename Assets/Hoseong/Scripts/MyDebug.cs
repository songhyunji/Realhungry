using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyDebug : MonoBehaviour
{
    void Awake()
    {
        debugText = GameObject.Find("Debug").GetComponent<Text>();
    }

    private static Text debugText;

    public static void Log(object o)
    {
        string txt = o.ToString();

        debugText.text = txt;
    }
}
