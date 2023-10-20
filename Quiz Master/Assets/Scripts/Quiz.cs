using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmpQuestionText;
    [SerializeField] QuestionSO qsoQuestion;
    [SerializeField] GameObject[] goAnswerButtons;
    [SerializeField] Sprite sprDefaultAnswerSprite;
    [SerializeField] Sprite sprCorrectAnswerSprite;
    int intCorrectAnswerIndex;


    private void Start()
    {
        GetNextQuestion();
        //DisplayQuestion();
    }
    public void OnAnswerSelected(int index)
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
        SetButtonState(false);
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
