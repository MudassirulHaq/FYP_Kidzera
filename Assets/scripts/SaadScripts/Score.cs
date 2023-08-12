using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score, highscore;
    public Text scoreText, highscoreText;

    // Start is called before the first frame update
    private void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        highscoreText.text = highscore.ToString();

        if (score > highscore)
        {
            highscore = score;
            Save();
        }
        
    }


    public void AddScore(int amount)
    {
        score += amount;
        Save();
    }

    public void SubtractScore(int amount)
    {
        score -= amount;
        Save();
    }

    public void Save()

    {
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("highscore", highscore); 
    }

    public void Load()
    {
        score = PlayerPrefs.GetInt("score");
        highscore = PlayerPrefs.GetInt("highscore");
    }
}
