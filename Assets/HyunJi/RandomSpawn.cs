using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
	public GameObject[] ingredients = new GameObject[10];
	int ing_num;

    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
		ing_num = Random.Range(0, 5);
    }

	IEnumerator Spawn()
	{
		Instantiate(ingredients[ing_num], new Vector3(0, 5, 0), Quaternion.identity);
		yield return new WaitForSeconds(3.0f);
		StartCoroutine(Spawn());
	}
}
