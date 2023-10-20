using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Auth;
using System;

public class DatabaseManager : MonoBehaviour
{
    private DatabaseReference _dbReference;
    private FirebaseUser _user;

    private void Awake()
    {
        _dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        _user = FirebaseAuth.DefaultInstance.CurrentUser;
    }

    public void SetDatabaseScore(int score)
    {
        _dbReference.Child("users").Child(_user.UserId).Child("score").SetValueAsync(score);
    }

    public void GetUserHighscore(Action<int>onComplete)
    {
        StartCoroutine(GetUserHighscoreCoroutine(onComplete));
    }

    private IEnumerator GetUserHighscoreCoroutine(Action<int> onComplete)
    {
        var task = _dbReference.Child("users").Child(_user.UserId).Child("score").GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.LogError("Error when getting highscore");
        }
        else
        {
            DataSnapshot snapshot = task.Result;

            if (snapshot.Value != null)
            {
                int highscore = int.Parse(snapshot.Value.ToString());

                onComplete?.Invoke(highscore);

            } else
            {
                onComplete?.Invoke(0);
            }
        }
    }

    public void GetScores(Action<List<Score>>onComplete)
    {
        StartCoroutine(GetScoresCoroutine(onComplete));
    }
    
    private IEnumerator GetScoresCoroutine(Action<List<Score>>onComplete)
    {
        List<Score> scores = new List<Score>();

        var task = _dbReference.Child("users").GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.LogError("Error when updating scoreboard");
        } else
        {

            DataSnapshot snapshot = task.Result;

            foreach (DataSnapshot childSnapshot in snapshot.Children)
            {
                if (childSnapshot.HasChild("score") && childSnapshot.HasChild("username"))
                {
                    var usernameValue = childSnapshot.Child("username").Value;
                    var scoreValue = childSnapshot.Child("score").Value;
                    if (usernameValue != null && scoreValue != null)
                    {
                        string name = usernameValue.ToString();
                        int score;
                        if (int.TryParse(scoreValue.ToString(), out score))
                        {
                            scores.Add(new Score(name, score));
                        }
                    }
                }
            }
            onComplete?.Invoke(scores);
        }
    }
}