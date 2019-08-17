using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSatisfy : MonoBehaviour
{
    public int Satisfy
    {
        get
        {
            return _satisfy;
        }

        set
        {
            if (value <= 0)
            {
                _satisfy = 0;
            }
            else if (value > maxSatisfy)
            {
                _satisfy = maxSatisfy;

                // 모드 돌입
                IsSatisfiedMode = true;
            }
            else
            {
                _satisfy = value;
            }

            float uiValue = (float)_satisfy / maxSatisfy;
            _satisfyUpdateEvent.Invoke(uiValue);
        }
    }

    public bool IsSatisfiedMode
    {
        get
        {
            return _isSatisfiedMode;
        }

        set
        {
            if (_isSatisfiedMode == value) return;

            _isSatisfiedMode = value;
            _satisfyModeEvent.Invoke(value);
            
            if(_isSatisfiedMode)
            {
                StopCoroutine("DecreaseSatisfy");
                StartCoroutine("SatisfiedMode");
            }
            else
            {
                StartCoroutine("DecreaseSatisfy");
            }
        }
    }

    public int _satisfy = 0;
    public int maxSatisfy = 1000;
    public int decreaseAmount = 5;
    public int satisfiedTime = 10;
    public MyFloatEvent _satisfyUpdateEvent;
    public MyBoolEvent _satisfyModeEvent;

    private bool _isSatisfiedMode = false;

    private void Start()
    {
        Satisfy = maxSatisfy;
        _isSatisfiedMode = false;

        StartCoroutine("DecreaseSatisfy");
    }

    IEnumerator DecreaseSatisfy()
    {
        while(Satisfy > 0)
        {
            if (!IsSatisfiedMode)
                Satisfy -= decreaseAmount;

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator SatisfiedMode()
    {
        yield return new WaitForSeconds(satisfiedTime);

        Satisfy = 500;
        IsSatisfiedMode = false;
    }

    private int levelIncreaseTime = 30;

    IEnumerator IncreaseLevel()
    {
        while(true)
        {
            yield return new WaitForSeconds(levelIncreaseTime);

            decreaseAmount += 1;
            if (satisfiedTime > 5) satisfiedTime--;
        }
    }
}
