using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using UnityEngine.UI;
public class ScoreBoardMgr : MonoBehaviour
{
    public Text scoretext;
    public Transform scoreboardContent;
    public GameObject scoreEntryPrefab;
    DatabaseReference databaseRef;
    void Start()
    {
        databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    public void SubmitScore(string UserName, int scoreCount)
    {
        string scoreKey = databaseRef.Child("scoreCounts").Push().Key;

        Dictionary<string, object> scoreData = new Dictionary<string, object>();
        scoreData["UserName"] = UserName;
        scoreData["scoreCount"]= scoreCount;

        databaseRef.Child("scoreCounts").Child(scoreKey).SetValueAsync(scoreData);
    }

    public void RetrieveScore()
    {
        DatabaseReference scoresRef = databaseRef.Child("scoreCounts");

        scoresRef.ValueChanged += HandleScoresValuseCanged;
    }

    void HandleScoresValuseCanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError("Error Retrieving Scores:"+args.DatabaseError.Message);
            return;
        }
        foreach(Transform child in scoreboardContent)
        {
            Destroy(child.gameObject);
        }
        foreach (DataSnapshot scoreSnapshot in args.Snapshot.Children)

        {
            string UserName = scoreSnapshot.Child("UserName").Value.ToString();
            int score = int.Parse(scoreSnapshot.Child("scoreCount").Value.ToString());

            GameObject scoreEntry = Instantiate(scoreEntryPrefab, scoreboardContent);
            scoreEntry.GetComponentInChildren<Text>().text = UserName + ":" + score.ToString();     
        }
        DataSnapshot scoresSnapshot = args.Snapshot;
    }


}
