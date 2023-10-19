using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;

public class AuthService
{
    private FirebaseAuth auth;
    private DatabaseReference databaseReference;

    public AuthService(FirebaseAuth auth, DatabaseReference databaseReference)
    {
        this.auth = auth;
        this.databaseReference = databaseReference;
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

                UpdateDatabaseUsername(newUser);


            });
        });
    }
    private void UpdateDatabaseUsername(FirebaseUser user)
    {
        databaseReference.Child("users").Child(user.UserId).Child("username").SetValueAsync(user.DisplayName).ContinueWithOnMainThread(task =>
        {

            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Error when updating username");
                return;
            }

            Debug.Log("Database Username updated successfully");

            SceneChanger.ChangeScene("LevelScene");

        });
    }
}
