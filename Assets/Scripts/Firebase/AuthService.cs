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
                PopUpManager.instance.ShowMessage("Please check your credentials.");
                return;
            }
            if (task.IsFaulted)
            {
                PopUpManager.instance.ShowMessage("Please check your credentials.");
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
                PopUpManager.instance.ShowMessage("This email is already in use");
                return;
            }

            FirebaseUser newUser = task.Result.User;

            UserProfile profile = new UserProfile { DisplayName = username };

            newUser.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task =>
            {

                if(task.IsCanceled || task.IsFaulted)
                {
                    Debug.Log("Update Profile Error: " + task.Exception);
                    return;
                }

                Debug.Log("Username Set Successfully");

                SceneChanger.ChangeScene("LevelScene");

            });
            

        });
    }
}
