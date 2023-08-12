using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShuffleList
{
    public static List<E> ShuffleListItems<E>(List<E> inputList)
    {
        List<E> originalList = new List<E>();
        originalList.AddRange(inputList);
        List<E> randomList = new List<E>();

        System.Random r = new System.Random();
        int randomIndex = 0;
        while (originalList.Count > 0)
        {
            randomIndex = r.Next(0, originalList.Count); //Choosing a random object in list
            randomList.Add(originalList[randomIndex]); //adding it to the new, random list
            originalList.RemoveAt(randomIndex); //removing to avoid copys
        }

        return randomList; //return the new random list
    }
}
