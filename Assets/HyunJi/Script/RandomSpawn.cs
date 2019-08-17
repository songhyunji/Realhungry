using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSpawn : MonoBehaviour
{
	public GameObject[] ingredients = new GameObject[10];
	int ing_num;
	float posX, posY;
	bool timecheck, effectend;

	public float x;

	public float animTime = 2f;         // Fade 애니메이션 재생 시간 (단위:초).  
	public Image fadeImage;            // UGUI의 Image컴포넌트 참조 변수.  
	private float start = 0f;           // Mathf.Lerp 메소드의 첫번째 값.  
	private float end = 1f;             // Mathf.Lerp 메소드의 두번째 값.  
	private float time = 0f;            // Mathf.Lerp 메소드의 시간 값.  
	private bool isPlaying = false;     // Fade 애니메이션의 중복 재생을 방지하기위해서 사용.

	// Start is called before the first frame update
	void Start()
	{
		x = 0.01f; timecheck = false; effectend = false;
		StartCoroutine(TimeCheck());
		StartCoroutine(Spawn());
		StartCoroutine(FadeIn());
	}

	// Update is called once per frame
	void Update()
	{
		ing_num = Random.Range(0, 5);
		posX = Random.Range(-10f, 10f);
		posY = Random.Range(0, 5f);

		if(timecheck)
		{
			x += 0.001f;
		}
	}

	IEnumerator Spawn()
	{
		GameObject instant = Instantiate(ingredients[ing_num], new Vector3(posX, posY, 0), Quaternion.identity);
		instant.transform.parent = gameObject.transform;
		if(!effectend)
		{
			StartCoroutine(DecCount());
		}
		yield return new WaitForSecondsRealtime(2.0f);
		Destroy(instant);
	}

	IEnumerator DecCount()
	{
		yield return new WaitForSecondsRealtime(x);
		StartCoroutine(Spawn());
	}

	IEnumerator TimeCheck()
	{
		yield return new WaitForSecondsRealtime(7.0f);
		timecheck = true;
		yield return new WaitForSecondsRealtime(2.0f);
		effectend = true;
		yield return new WaitForSecondsRealtime(1.0f);
		StartCoroutine(FadeOut());
	}

	IEnumerator FadeIn()
	{
		for (float i = 0f; i < 0.5f; i += 0.05f)
		{
			Color color = new Vector4(0, 0, 0, i);
			fadeImage.color = color;
			yield return 0;
		}
	}

	IEnumerator FadeOut()
	{
		for (float i = 0.5f; i > 0f; i -= 0.05f)
		{
			Color color = new Vector4(0, 0, 0, i);
			fadeImage.color = color;
			yield return 0;
		}

		Destroy(this.gameObject);
	}
}
