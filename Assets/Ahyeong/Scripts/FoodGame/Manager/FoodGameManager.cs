using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodGameManager : MonoSingleton<FoodGameManager>
{
    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
            scoreUIEvent.Invoke(value);
        }
    }

    public GameObject overUI;
    public Text overScoreText;
    public MonsterSatisfy satisfy;
    public TestFoodMaker foodMaker;
    public Spawner spawner;
    public MyIntEvent scoreUIEvent;

    private int _score = 0;

    private void Start()
    {
        InitGame();
        StartGame();
    }

    public void InitGame()
    {
        spawner.Init();
        foodMaker.Init();
    }

    public void StartGame()
    {
        Score = 0;
        spawner.StartSpawn();
        foodMaker.ResetValue();
        satisfy.ResetValues();
        IngredientObject.ResetObject();
        overUI.SetActive(false);
    }

    public void EndGame()
    {
        spawner.Stop();
        overUI.SetActive(true);
        overScoreText.text = Score.ToString();
    }

    public void AddSatisfy(int value)
    {
        satisfy.Satisfy += value;
    }
}
