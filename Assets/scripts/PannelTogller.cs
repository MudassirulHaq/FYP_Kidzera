using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PannelTogller : MonoBehaviour
{
    bool isOn = false;
    [SerializeField] GameObject Pannel;

    public void togglePannel()
    {
        if(isOn)
        {
            Pannel.SetActive(false);
            isOn = false;
        }
        else
        {
            
            Pannel.SetActive(true);
            isOn = true;
        }
    }
}
