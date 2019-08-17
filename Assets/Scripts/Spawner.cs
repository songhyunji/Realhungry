using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public FoodIngredientDatabase ingDB;
    public Image nextIng_Image;
    public Text nextIng_Text;
    public IngredientObject prefab;

    public float spawnCoolTime = 2f;

    public void Init()
    {
        IngredientObject.Init_Ob(prefab);

        TestFoodMaker.Instance.onFeverStart += new OnFeverStart(FeverStart);
        TestFoodMaker.Instance.onFeverEnd += new OnFeverEnd(FeverEnd);
    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnIngRoutine());
        StartCoroutine(SpawningTrash());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    FoodIngredient next_ing;

    IEnumerator SpawnIngRoutine()
    {
        while (true)
        {
            next_ing = ingDB.GetRandomIngredient();
            nextIng_Image.sprite = next_ing.sprite;

            yield return new WaitForSeconds(spawnCoolTime);

            FoodIngredient ing = next_ing;


            var ing_Object = IngredientObject.Pull_Ob();

            Vector3 x_offset = new Vector3(Random.Range(-0.1f, 0.1f), 0f, 0f);

            ing_Object.transform.position = transform.position + x_offset;
            ing_Object.Init(ing);
        }

    }

    IEnumerator SpawningTrash()
    {
        int randTime = 0;
        while (true)
        {
            FoodIngredient next_trash = ingDB.GetRandomTrash();

            randTime = Random.Range(5, 16);
            yield return new WaitForSeconds(randTime);

            var ing_Object = IngredientObject.Pull_Ob();

            Vector3 x_offset = new Vector3(Random.Range(-0.1f, 0.1f), 0f, 0f);

            ing_Object.transform.position = transform.position + x_offset;
            ing_Object.Init(next_trash);
        }

    }

    void FeverStart()
    {
        spawnCoolTime = 1f;
    }

    void FeverEnd()
    {
        spawnCoolTime = 2f;
    }
}
