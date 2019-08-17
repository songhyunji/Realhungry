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

    private const float spawnCoolTime = 2f;

    public void Init()
    {
        IngredientObject.Init_Ob(prefab);
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
            nextIng_Text.text = next_ing.nameStr;

            yield return new WaitForSeconds(spawnCoolTime);

            FoodIngredient ing = next_ing;

            var ing_Object = IngredientObject.Pull_Ob();
            ing_Object.transform.position = transform.position;
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
            ing_Object.transform.position = transform.position;
            ing_Object.Init(next_trash);
        }

    }
}
