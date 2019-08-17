using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
	public GameObject[] ingredients = new GameObject[10];
	int ing_num;
	float posX, posY;
	float scale = 1;

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(DecScale());
	}

	// Update is called once per frame
	void Update()
	{
		ing_num = Random.Range(0, 5);
		posX = Random.Range(-10f, 10f);
		posY = Random.Range(0, 5f);

		StartCoroutine(Spawn());
	}

	IEnumerator Spawn()
	{
		GameObject instant = Instantiate(ingredients[ing_num], new Vector3(posX, posY, 0), Quaternion.identity);
		instant.transform.localScale = new Vector3(scale, scale, scale);
		instant.transform.parent = gameObject.transform;
		yield return new WaitForSecondsRealtime(2.0f);
		Destroy(instant);
	}

	IEnumerator DecScale()
	{
		if(scale > 0)
		{
			yield return new WaitForSecondsRealtime(1.0f);
			scale -= 0.2f;
			StartCoroutine(DecScale());
		}
		else
		{
			Destroy(this.gameObject);
		}

	}
}
