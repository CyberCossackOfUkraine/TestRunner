using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase;

public class AuthService
{
    private FirebaseAuth auth;

    public AuthService(FirebaseAuth auth)
    {
        this.auth = auth;
    }

    public void SignIn(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Sign In Failed. Canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("Sign In Error: " + task.Exception);
                return;
            }
            AuthResult result = task.Result;
            

            Debug.Log("Sign In Result: " + result.User.DisplayName + " , " + result.User.UserId);

            SceneChanger.ChangeScene("LevelScene");
        });
    }

    public void SignUp(string username, string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Sign Up Canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("Sign Up Error: " + task.Exception);
                return;
            }

            AuthResult result = task.Result;
            Debug.Log("Sign Up Result: " + result.User.DisplayName + " , " + result.User.UserId);
            
        });
    }
}
