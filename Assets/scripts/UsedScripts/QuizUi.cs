using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizUi : MonoBehaviour
{
    [SerializeField]private QuizMgr quizMgr;
    [SerializeField]private Text questionText, scoreText, timerText;
    [SerializeField]private List<Image> LifeImageList;
    [SerializeField]private GameObject gameoverP, mainmenuP, gameMenuP, gameComP;
    

    [SerializeField]private List<Button> options, playButton;
    [SerializeField]private Color correctcol, wrongcol, normalcol, lifeCol;

    private Question question;
    private bool answered;

    public Text ScoreText {get {return scoreText;}}
    public Text TimerText {get {return timerText;}}

    public GameObject GameOverPanel {get{return gameoverP;}}
    public GameObject GameCompletePanel {get{return gameComP;}}
    
    


    
    // Start is called before the first frame update
    void Awake()
    {
        for (int i=0; i<options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(()=> Onclick(localBtn));
        }

         for (int i=0; i<playButton.Count; i++)
        {
            Button localBtn = playButton[i];
            localBtn.onClick.AddListener(()=> Onclick(localBtn));
        }
    }

    public void SetQuestion(Question question)
    {
        this.question = question;
        questionText.text = question.questionInfo;

        List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);

        for (int i = 0; i<options.Count;i++)
        {
            options[i].GetComponentInChildren<Text>().text= answerList[i];
            options[i].name = answerList[i];
            options[i].image.color = normalcol;

        }
        answered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Onclick(Button btn)
    {
        if(quizMgr.GameStatus == GameStatus.Playing)
     {
        if(!answered)
        {
            answered = true;
            bool val = quizMgr.Answer(btn.name);

            if(val)
            {
                btn.image.color = correctcol;

            }
            else
            {
                btn.image.color = wrongcol;
            }
        }
     }
     switch (btn.name)
        {
            case "Play":
                quizMgr.StartGame(0);
                mainmenuP.SetActive(false);
                gameMenuP.SetActive(true);

            break;
        }
    }
    public void RetryBtn()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReduceLife(int index)
    {
        LifeImageList[index].color = lifeCol;
    }
   
}
