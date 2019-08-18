using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
            scoreUIEvent.Invoke(value * 10);
        }
    }

    public GameObject pauseUI;
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
        Time.timeScale = 1f;
        Score = 0;
        spawner.StartSpawn();
        foodMaker.ResetValue();
        satisfy.ResetValues();
        IngredientObject.ResetObject();
        overUI.SetActive(false);
    }

    public void EndGame()
    {
        foodMaker.EndGame();
        spawner.Stop();
        overUI.SetActive(true);
        overScoreText.text = (Score * 10).ToString();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseUI.SetActive(true);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
    }

    public void GoHome()
    {
        SceneManager.LoadScene("Title");
    }

    public void AddSatisfy(int value)
    {
        satisfy.Satisfy += value;
    }
}
