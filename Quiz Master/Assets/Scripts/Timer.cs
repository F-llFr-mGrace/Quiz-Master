using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float fltTimeToCompleteQuestion =  30f;
    [SerializeField] float fltTimeToShowCorrectAnswer = 10f;
    public bool boolIsAnsweringQuestion = false;
    public bool boolLoadNextQuestion;
    public float fltFillFraction;
    float fltTimerValue;

    private void Update()
    {
        UpdateTimer();
    }
    public void CancelTimer()
    {
        fltTimerValue = 0f;
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
            else
            {
                fltFillFraction = fltTimerValue / fltTimeToCompleteQuestion;
            }
        }
        else
        {
            if (fltTimerValue <= 0)
            {
                boolIsAnsweringQuestion = true;
                fltTimerValue = fltTimeToCompleteQuestion;
                boolLoadNextQuestion = true;
            }
            else
            {
                fltFillFraction = fltTimerValue / fltTimeToShowCorrectAnswer;
            }
        }

        Debug.Log(boolIsAnsweringQuestion + ": " + fltTimerValue + " = " + fltFillFraction);
    }
}
