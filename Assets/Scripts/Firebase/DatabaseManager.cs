using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions;
using System;
using UnityEngine.SocialPlatforms.Impl;

public class DatabaseManager : MonoBehaviour
{
    private DatabaseReference _dbReference;
    private FirebaseUser _user;

    public static DatabaseManager instance;

    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }

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
                if (childSnapshot.HasChild("score"))
                {
                    string name = childSnapshot.Child("username").Value.ToString();
                    int score = int.Parse(childSnapshot.Child("score").Value.ToString());
                    scores.Add(new Score(name, score));

                }
            }
                onComplete?.Invoke(scores);
        }


    }



}
