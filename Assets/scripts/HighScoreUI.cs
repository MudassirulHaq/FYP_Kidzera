using UnityEngine;
using UnityEngine.UI;

public class HighscoreUI : MonoBehaviour
{
    public InputField playerNameInputField;
    public Text scoreText;
    public HighscoreMgr highscoreMgr;

    private int currentScore = 0;

    private void Start()
    {
        highscoreMgr = FindObjectOfType<HighscoreMgr>();
        UpdateScoreText();
    }

    public void SaveScore()
    {
        string playerName = playerNameInputField.text;
        highscoreMgr.SaveScore(playerName, currentScore);
    }

    public void UpdateScore()
    {
        string playerName = playerNameInputField.text;
        int newScore = currentScore + 10; // Example: Increment the score by 10
        highscoreMgr.UpdateScore(playerName, newScore);
        currentScore = newScore;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + currentScore.ToString();
    }
}
