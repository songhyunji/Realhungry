using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotButton : MonoBehaviour
{
	int i;
    // Start is called before the first frame update
    void Start()
    {

	}

    // Update is called once per frame
    void FixedUpdate()
    {
		transform.localEulerAngles = new Vector3(0, 0, -Mathf.PingPong(Time.time * 100, 10));
		transform.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time * 100, 10));
	}
}
