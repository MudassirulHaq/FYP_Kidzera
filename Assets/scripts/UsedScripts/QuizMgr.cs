using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;


public class QuizMgr : MonoBehaviour

    
{   [SerializeField] private float timeLimit = 30;
    [SerializeField] private QuizUi quizUi;
    [SerializeField]
   
    private List<Question> questions;
    private Question selectedQuestion;
    public  int scoreCount  ;
    private float currentTime;
    private int lifeleft = 3;
    public  int highscore;
   
    private GameStatus gameStatus = GameStatus.Next;
   // public Text scoreText,highscoreText;

    public GameStatus GameStatus {get{return gameStatus;}}
    // Start is called before the first frame update
    public void StartGame(int index)

    {
        
     
        //scoreCount = 0;
        currentTime = timeLimit;
        lifeleft = 3;

        SelectQuestion();
        gameStatus = GameStatus.Playing;
    }

    // Update is called once per frame
    void SelectQuestion()
    {
        int val = UnityEngine.Random.Range(0, questions.Count);
        selectedQuestion = questions[val];

        quizUi.SetQuestion(selectedQuestion);

        questions.RemoveAt(val);
    }

    private void Update()
    {
        if (gameStatus == GameStatus.Playing)
            {
                currentTime -= Time.deltaTime;
                SetTimer(currentTime);
            }
      //     scoreText.text = scoreCount.ToString();
      //  highscoreText.text = highscore.ToString();

      //  if (scoreCount > highscore)
       // {
         //   highscore = scoreCount;
        //    Save();
       // }
    }

    private void SetTimer(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value);
        quizUi.TimerText.text = "Time" + time.ToString("mm':'ss");

        if(currentTime <=0)
            {
                  gameStatus = GameStatus.Next;
                quizUi.GameOverPanel.SetActive(true);
            }
    }

    public bool Answer(string answered)
    {
        bool correctAnswer = false;

        if( answered == selectedQuestion.correctAnswer)
        {
            //yes or correct
            correctAnswer = true;
            scoreCount += 10;
            quizUi.ScoreText.text = "Score:" + scoreCount;
        }
        else
        {   
            //no wrong
            lifeleft--;
            quizUi.ReduceLife(lifeleft);

            if(lifeleft <=0)
            {
                gameStatus = GameStatus.Next;
                quizUi.GameOverPanel.SetActive(true);
               
            }
        }
        if(gameStatus == GameStatus.Playing)
        {
            if(questions.Count >0)
            {
                 Invoke("SelectQuestion",0.5f);
            }
            else
            {
                gameStatus = GameStatus.Next;
                quizUi.GameCompletePanel.SetActive(true);
            }
        
       
        
        }
        return correctAnswer;
    }
    public void Save()

    {
        PlayerPrefs.SetInt("score", scoreCount);
        if(scoreCount > highscore)
        {
        PlayerPrefs.SetInt("highscore", highscore);
        }
    }

    //public void Load()
    //{
      //  scoreCount = PlayerPrefs.GetInt("score");
        //highscore = PlayerPrefs.GetInt("highscore");

    
//}
}
    

[System.Serializable]
public class Question
{
    public string questionInfo;
    public List<string> options;
    public string correctAnswer;
    public QuestionType questionType;
}
[System.Serializable]
public enum QuestionType
{
    TEXT
    
}
[System.Serializable]
public enum GameStatus
{
    Next,
    Playing
}



