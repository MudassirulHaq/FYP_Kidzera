using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonR : MonoBehaviour
{
public QuizMgr quizMgr;
 public int activeScore;
public int focusedScore;
public int lazyScore;
public int confusedScore;
 void CalculatePersonality()
{
    if (quizMgr.scoreCount >= 60)
    {
        activeScore = Random.Range(6, 10);
        focusedScore = Random.Range(6, 10);
        lazyScore = Random.Range(1, 5);
        confusedScore = Random.Range(1, 5);
    }
    else if (quizMgr.scoreCount<= 50)
    {
        activeScore = Random.Range(1, 6);
        focusedScore = Random.Range(1, 6);
        lazyScore = Random.Range(5, 10);
        confusedScore = Random.Range(5, 9);
    }
}
 

   
}
