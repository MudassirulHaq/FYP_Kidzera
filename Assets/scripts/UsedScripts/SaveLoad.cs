using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour

{

    public Text activeText,focusedText,lazyText,confusedText,scoreTextL, scoreZText;
     public Text activeTextZ,focusedTextZ,lazyTextZ,confusedTextZ;
    // public Text highscoreText,highscoreZText;
    public QuizMgr quizMgr;
    public GameManager gameManager;
    public int activeScore,focusedScore,lazyScore,confusedScore;
     public int activeScoreZ,focusedScoreZ,lazyScoreZ,confusedScoreZ;
    

  

    // Update is called once per frame
     void Update()
    {
          scoreTextL.text = quizMgr.scoreCount.ToString();
       // highscoreText.text = quizMgr.highscore.ToString();

        //quizMgr.highscore = quizMgr.scoreCount;

        scoreZText.text = gameManager.score.ToString();
        
        

       // if (quizMgr.scoreCount > quizMgr.highscore)
       // {
            
           // quizMgr.Save();

           
       // }
        activeTextZ.text = "Active: " + activeScoreZ.ToString();
        focusedTextZ.text = "Focused: " + focusedScoreZ.ToString();
        lazyTextZ.text = "Strong: " + lazyScoreZ.ToString();
        confusedTextZ.text = "Confused: " +confusedScoreZ.ToString();

        activeText.text = "Active: " + activeScore.ToString();
        focusedText.text = "Focused: " + focusedScore.ToString();
        lazyText.text = "Strong: " + lazyScore.ToString();
        confusedText.text = "Confused: " +confusedScore.ToString();
    }
   //  public void Save()

    //{
     //   PlayerPrefs.SetInt("score", quizMgr.scoreCount);
       // PlayerPrefs.SetInt("highscore", quizMgr.highscore); 
    //}

    public void Load()
    {
        quizMgr.scoreCount = PlayerPrefs.GetInt("score");
       // quizMgr.highscore = PlayerPrefs.GetInt("highscore");
        //gameManager.score = PlayerPrefs.GetInt("scoreZ");
        CalculatePersonality();
        

    }
    
    public void LoadZoo()
    {
        gameManager.score = PlayerPrefs.GetInt("scoreZ");
        CalculatePersonalityZoo();
        

    }
    
     void CalculatePersonality()
{
    if (quizMgr.scoreCount >=60)
    {
        activeScore = Random.Range(6, 10);
        focusedScore = Random.Range(6, 10);
        lazyScore = Random.Range(1, 5);
        confusedScore = Random.Range(1, 5);
    }
    else if (quizMgr.scoreCount <=50 )
    {
        activeScore = Random.Range(1, 6);
        focusedScore = Random.Range(1, 6);
        lazyScore = Random.Range(5, 10);
        confusedScore = Random.Range(5, 9);
    }
}

     void CalculatePersonalityZoo()
{
    if (gameManager.score >=140)
    {
        activeScoreZ = Random.Range(6, 10);
        focusedScoreZ = Random.Range(6, 10);
        lazyScoreZ = Random.Range(1, 5);
        confusedScoreZ = Random.Range(1, 5);
    }
    else if (gameManager.score <=130 )
    {
        activeScoreZ = Random.Range(1, 6);
        focusedScoreZ = Random.Range(1, 6);
        lazyScoreZ = Random.Range(5, 10);
        confusedScoreZ = Random.Range(5, 9);
    }
}
}
