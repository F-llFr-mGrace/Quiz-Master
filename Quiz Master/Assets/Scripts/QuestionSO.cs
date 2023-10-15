using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] string strQuestion = "Enter question text";
    [SerializeField] string[] strAnswers = new string[4];
    [SerializeField] int intCorrectAnswerIndex;

    public string GetQuestion() 
    { 
        return strQuestion; 
    }

    public string GetAnswers(int index)
    {
        return strAnswers[index];
    }

    public int GetCorrectAnswerIndex()
    {
        return intCorrectAnswerIndex;
    }
}