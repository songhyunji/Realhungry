using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSatisfyAdd : MonoBehaviour
{
    public int amount = 10;

    public void AddSatisfy()
    {
        FoodGameManager.Instance.satisfy.Satisfy += amount;
        FoodGameManager.Instance.Score += amount * 10;
    }
}
