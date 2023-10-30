using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI tmpQuestionText;
    [SerializeField] List<QuestionSO> listQuestions = new List<QuestionSO>();
    QuestionSO qsoCurrentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] goAnswerButtons;
    int intCorrectAnswerIndex;
    bool boolHasAnsweredEarly = true;

    [Header("Button Colours")]
    [SerializeField] Sprite sprDefaultAnswerSprite;
    [SerializeField] Sprite sprCorrectAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image imgTimerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI tmpScoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider sldrProgressBar;
    public bool boolIsComplete = false;

    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        sldrProgressBar.maxValue = listQuestions.Count;
        sldrProgressBar.value = 0;
    }
    private void Update()
    {
        imgTimerImage.fillAmount = timer.fltFillFraction;
        if (timer.boolLoadNextQuestion)
        {
            if (sldrProgressBar.value == sldrProgressBar.maxValue)
            {
                boolIsComplete = true;
                return;
            }
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
        tmpScoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }
    private void DisplayAnswer(int index)
    {
        Image buttonImage;

        if (index == qsoCurrentQuestion.GetCorrectAnswerIndex())
        {
            tmpQuestionText.text = "Correct!";
            buttonImage = goAnswerButtons[index].GetComponentInChildren<Image>();
            buttonImage.sprite = sprCorrectAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            intCorrectAnswerIndex = qsoCurrentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = qsoCurrentQuestion.GetAnswers(intCorrectAnswerIndex);
            tmpQuestionText.text = "Sorry, the correct answer is:\n" + correctAnswer;
            buttonImage = goAnswerButtons[intCorrectAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = sprCorrectAnswerSprite;
        }
    }
    private void GetNextQuestion()
    {
        if (listQuestions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            sldrProgressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
    }
    private void GetRandomQuestion()
    {
        int index = Random.Range(0, listQuestions.Count);
        qsoCurrentQuestion = listQuestions[index];

        if (listQuestions.Contains(qsoCurrentQuestion))
        {
            listQuestions.Remove(qsoCurrentQuestion);
        }
    }
    private void DisplayQuestion()
    {
        tmpQuestionText.text = qsoCurrentQuestion.GetQuestion();

        for (int i = 0; i < goAnswerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = goAnswerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = qsoCurrentQuestion.GetAnswers(i);
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