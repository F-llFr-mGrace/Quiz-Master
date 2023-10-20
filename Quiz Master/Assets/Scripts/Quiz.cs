using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Reflection;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI tmpQuestionText;
    [SerializeField] QuestionSO qsoQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] goAnswerButtons;
    int intCorrectAnswerIndex;
    bool boolHasAnsweredEarly;

    [Header("Button Colours")]
    [SerializeField] Sprite sprDefaultAnswerSprite;
    [SerializeField] Sprite sprCorrectAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image imgTimerImage;
    Timer timer;


    private void Start()
    {
        timer = FindObjectOfType<Timer>();
        GetNextQuestion();
        //DisplayQuestion();
    }
    private void Update()
    {
        imgTimerImage.fillAmount = timer.fltFillFraction;
        if (timer.boolLoadNextQuestion)
        {
            boolHasAnsweredEarly = false;
            GetNextQuestion();
            timer.boolLoadNextQuestion = false;
        }
        else if (!boolHasAnsweredEarly && !timer.boolIsAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }
    public void OnAnswerSelected(int index)
    {
        boolHasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
    }
    private void DisplayAnswer(int index)
    {
        Image buttonImage;

        if (index == qsoQuestion.GetCorrectAnswerIndex())
        {
            tmpQuestionText.text = "Correct!";
            buttonImage = goAnswerButtons[index].GetComponentInChildren<Image>();
            buttonImage.sprite = sprCorrectAnswerSprite;
        }
        else
        {
            intCorrectAnswerIndex = qsoQuestion.GetCorrectAnswerIndex();
            string correctAnswer = qsoQuestion.GetAnswers(intCorrectAnswerIndex);
            tmpQuestionText.text = "Sorry, the correct answer is:\n" + correctAnswer;
            buttonImage = goAnswerButtons[intCorrectAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = sprCorrectAnswerSprite;
        }
    }
    private void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefaultButtonSprites();
        DisplayQuestion();
    }
    private void DisplayQuestion()
    {
        tmpQuestionText.text = qsoQuestion.GetQuestion();

        for (int i = 0; i < goAnswerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = goAnswerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = qsoQuestion.GetAnswers(i);
        }
    }
    private void SetButtonState(bool state)
    {
        for (int i = 0; i < goAnswerButtons.Length; i++)
        {
            Button button = goAnswerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
    private void SetDefaultButtonSprites()
    {
        for (int i = 0; i < goAnswerButtons.Length; i++)
        {
            Image buttonImage = goAnswerButtons[i].GetComponent<Image>();
            buttonImage.sprite = sprDefaultAnswerSprite;
        }
    }
}
