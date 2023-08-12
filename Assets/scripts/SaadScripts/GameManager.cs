using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System.Linq;
using TMPro;

public class GameManager : MonoBehaviour
{
   // [SerializeField] private float TimeL = 30;
    //[SerializeField] private Text timerTextZ;
    [SerializeField] private GameObject gameoverPZ;
    //public float timeP;
    public TextMeshProUGUI timeText;
    public GameObject quizScreen;
    public GameObject[] buttons;
    public TextMeshProUGUI scoreDisplay;

    public Sprite neutralBtnSprite;
    public Sprite correctBtnSprite;
    public Sprite incorrectBtnSprite;

    GameObject correctBtn;
    public int score;
    public int hs;
    //public Text TimerTextZ {get {return timerTextZ;}}
    public GameObject GameOverPanelZoo{get{return gameoverPZ;}}

    // private GameStatusZ gameStatusZ = GameStatusZ.Next;
   // public Text scoreText,highscoreText;

    //public GameStatusZ GameStatusZ {get{return gameStatusZ;}}
    

    
    


    Animals selectedAnimal;

    GameState state;
    float totalTime = 180f;
    float remainingTime;
        

    public static GameManager Instance;

    void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
   // public void StartGameZoo(int index)
    //{
   //     timeP = TimeL;
   //     gameStatusZ = GameStatusZ.Playing;
   // }
    void Start()
    {
        score = 0;
        remainingTime = totalTime;
      //  timeP = TimeL;
        foreach (GameObject button in buttons)
        {
            button.GetComponent<Button>().onClick.AddListener(() => OnButtonClicked(button));
        }
        UpdateGameState(GameState.SelectAnimal);
    }

   //  private void Update()
   // {
   //     if (gameStatusZ == GameStatusZ.Playing)
    //        {
   //             timeP -= Time.deltaTime;
    //            SetTimers(timeP);
     //       }
      
   // }
   // private void Update()
   // {
   //   if(state == GameState.Playing)
   //   {
   //     timeP -= Time.deltaTime;
   //     SetTimers(timeP);
   //   }
   // }

   //  private void SetTimers(float value)
   // {
   //     TimeSpan time = TimeSpan.FromSeconds(value);
   //     TimerTextZ.text = "Time" + time.ToString("mm':'ss");

   //     if(timeP <=0)
   //         {
   //               gameStatusZ = GameStatusZ.Next;
    //            GameOverPanelZoo.SetActive(true);
   //         }
   // }

    public void UpdateGameState(GameState newState)
    {
        state = newState;
        
        switch (newState)
        {
            case GameState.SelectAnimal:
                HandleSelectAnimal();
                break;
            case GameState.SelectAnimalOption:
                HandleSelectAnimalOption();
                break;
            case GameState.QuizOptionSelected:
                HandleQuizOptionSelected();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        Debug.Log("newState " + newState.ToString());
    }

    private void HandleQuizOptionSelected()
    {
        scoreDisplay.text = score.ToString();
        StartCoroutine(SwitchToSelectAnimalState());
    }

    private void HandleSelectAnimal()
    {
        quizScreen.SetActive(false);
    }

    void HandleSelectAnimalOption()
    {
        int selectedButtonNum = Random.Range(0, buttons.Length - 1);
        buttons[selectedButtonNum].GetComponentInChildren<TextMeshProUGUI>().text = selectedAnimal.ToString();
        correctBtn = buttons[selectedButtonNum];
        
        // Get all animals from the animals enum as a list
        List<Animals> remainingAnimalOptions = Enum.GetValues(typeof(Animals)).Cast<Animals>().ToList();

        remainingAnimalOptions.Remove(selectedAnimal);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i != selectedButtonNum)
            {
                int randIndex = Random.Range(0, remainingAnimalOptions.Count - 1);
                Animals randomlySelectedAnimal = remainingAnimalOptions[randIndex];
                remainingAnimalOptions.RemoveAt(randIndex);
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = randomlySelectedAnimal.ToString();
            } 
            buttons[i].GetComponent<Image>().sprite = neutralBtnSprite;
        }
        quizScreen.SetActive(true);
        StartCoroutine(StartTimer());
    }
    IEnumerator StartTimer()
    {
        while(remainingTime>0f)
        {
            remainingTime -= Time.deltaTime;
            DisplayTimer();
            yield return null;
        }
        remainingTime = 0f;
        DisplayTimer();
        HandleGameOver();
    }

    void DisplayTimer()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timeText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }

    void HandleGameOver()
    {
        GameOverPanelZoo.SetActive(true);
    }

    void OnButtonClicked(GameObject btn)
    {
        if (state == GameState.SelectAnimalOption)
        {
            if (btn == correctBtn)
            {
                btn.GetComponent<Image>().sprite = correctBtnSprite;
                score += 10;
            }
            else
            {
                btn.GetComponent<Image>().sprite = incorrectBtnSprite;
            }
            UpdateGameState(GameState.QuizOptionSelected);
        }
    }

    public void OnAnimalSelect(Animal animal)
    {
        if (state == GameState.SelectAnimal)
        {
            animal.audioSource.PlayOneShot(animal.animalSound);
            if (!animal.isSelected)
            {
                Debug.Log($"Animal Selected {animal.animalType}");
                animal.isSelected = true;
                selectedAnimal = animal.animalType;
                UpdateGameState(GameState.SelectAnimalOption);
            }
        };
    }

    IEnumerator SwitchToSelectAnimalState()
    {
        yield return new WaitForSeconds(1f);
        quizScreen.SetActive(false);
        UpdateGameState(GameState.SelectAnimal);
    }

    public void Save()

    {
        PlayerPrefs.SetInt("scoreZ", score);
       // if(score > hs)
       // {
       // PlayerPrefs.SetInt("highscore", hs);
       // }
    }

}

public enum GameState
{
    SelectAnimal,
    SelectAnimalOption,
    QuizOptionSelected,
    
}
//[System.Serializable]
//public enum GameStatusZ
//{
  //  Next,
  //  Playing
//}
