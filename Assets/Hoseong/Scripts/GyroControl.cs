using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControl : MonoBehaviour
{
    private const float ac_coef = 9.81f * 1.5f;
    public GameObject column;

    void Update()
    {
        float ac = Input.acceleration.x;
        if (ac > Mathf.Pow(0.5f, 0.5f)) ac = Mathf.Pow(0.5f, 0.5f);
        else if (ac < -Mathf.Pow(0.5f, 0.5f)) ac = -Mathf.Pow(0.5f, 0.5f);

        Vector2 vec = new Vector2(Mathf.Sin(Mathf.PI * 0.5f * ac), -Mathf.Cos(Mathf.PI * 0.5f * ac));
        vec = vec.normalized * ac_coef;
        Physics2D.gravity = vec;
        //MyDebug.Log(vec.x);
    }
}
