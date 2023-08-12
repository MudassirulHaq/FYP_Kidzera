using System;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public Animals animalType;
    public AudioClip animalSound;
    public bool isSelected = false;

    public AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void OnSelected()
    {
        GameManager.Instance.OnAnimalSelect(this);
    }

}
