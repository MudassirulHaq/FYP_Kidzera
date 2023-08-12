using Firebase;
using Firebase.Database;
using UnityEngine;

public class HighscoreMgr : MonoBehaviour
{
    
    private DatabaseReference databaseReference;

    private void Awake()
    {
        // Get a reference to the root of your Firebase database
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveScore(string profUserName_Text, int scoreCount)
    {
        // Create a new entry for the score with the player's name as the key
        databaseReference.Child("scores").Child(profUserName_Text).SetValueAsync(scoreCount);
    }

    public void UpdateScore(string profUserName_Text, int scoreCount)
    {
        // Update the existing score for the player
        databaseReference.Child("scores").Child(profUserName_Text).SetValueAsync(scoreCount);
    }
}
