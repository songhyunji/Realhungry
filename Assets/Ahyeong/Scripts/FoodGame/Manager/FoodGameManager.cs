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
    public MonsterSatisfy satisfy;
    public MyIntEvent scoreUIEvent;

    private int _score = 0;

    private void Start()
    {
        Score = 0;
    }

    public void AddSatisfy(int value)
    {
        satisfy.Satisfy += value;
    }
}
