using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonRUI : MonoBehaviour
{
    // Start is called before the first frame update
public PersonR personR;
public Text activeText;
public Text focusedText;
public Text lazyText;
public Text confusedText;

void Start()
{
    // Assign Active, Focused, Lazy, Confused scores to Text UI elements
    activeText.text = "Active: " + personR.activeScore.ToString();
    focusedText.text = "Focused: " + personR.focusedScore.ToString();
    lazyText.text = "Lazy: " + personR.lazyScore.ToString();
    confusedText.text = "Confused: " + personR.confusedScore.ToString();
}
}
