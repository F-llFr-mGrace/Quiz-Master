using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float fltTimeToCompleteQuestion =  30f;
    [SerializeField] float fltTimeToShowCorrectAnswer = 10f;
    public bool boolIsAnsweringQuestion = false;
    float fltTimerValue;

    private void Update()
    {
        UpdateTimer();
    }
    void UpdateTimer()
    {
        fltTimerValue -= Time.deltaTime;

        if (boolIsAnsweringQuestion)
        {
            if (fltTimerValue <= 0)
            {
                boolIsAnsweringQuestion = false;
                fltTimerValue = fltTimeToShowCorrectAnswer;
            }
        }
        else
        {
            if (fltTimerValue <= 0)
            {
                boolIsAnsweringQuestion = true;
                fltTimerValue = fltTimeToCompleteQuestion;
            }
        }

        Debug.Log(fltTimerValue);
    }
}
