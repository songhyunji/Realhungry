using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		Canvas canvas = GetComponent<Canvas>();
		Camera myCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		canvas.worldCamera = myCamera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
