using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		float i = 1 + (Mathf.PingPong(Time.time * 2, 1) * 0.5f);
        transform.localScale = new Vector3(i, i, 1);
	}
}
