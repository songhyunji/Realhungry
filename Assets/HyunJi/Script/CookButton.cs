using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CookButton : MonoBehaviour
{
	public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {

	}

    // Update is called once per frame
    void FixedUpdate()
    {
		transform.Rotate(new Vector3(0, 0, speed));
	}

	public void FaceBtnPress()
	{
		SceneManager.LoadScene("Tutorial");
	}
}
