using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenecontroller : MonoBehaviour
{
    public void GoToMM()
    {
        SceneManager.LoadScene("MM.ABC");
    }

    public void OutARBook()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

    public void ABCGame()
    {
        SceneManager.LoadScene("ARBook");
    }

    public void QuizABC()
    {
        SceneManager.LoadScene("AbcQuiz");
    }
    
    public void ARZOO()
    {
        SceneManager.LoadScene("MainScene1");
    }
    public void ScoreBoard()
    {
        SceneManager.LoadScene("LeaderBoard");
    }

    public void ProfilePage()
    {
        SceneManager.LoadScene("Firebase Registration");
    }
    public void Dashboard()
    {
        SceneManager.LoadScene("Dashboard");
    }
}
