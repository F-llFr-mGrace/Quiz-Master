using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmpFinalScoreText;
    ScoreKeeper scoreKeeper;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        tmpFinalScoreText.text = "Congratulations! /nYou got a score of" + scoreKeeper.CalculateScore() + "%";
    }
}
