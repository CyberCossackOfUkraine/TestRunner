using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Firebase.Database;

public class AuthManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject authPanel;
    [SerializeField] private GameObject registerPanel;

    [Header("Login")]
    [SerializeField] private InputField emailLoginInput;
    [SerializeField] private InputField passwordLoginInput;

    [Header("Register")]
    [SerializeField] private InputField usernameRegisterInput;
    [SerializeField] private InputField emailRegisterInput;
    [SerializeField] private InputField passwordRegisterInput;
    [SerializeField] private InputField passwordRepeatRegisterInput;

    private FirebaseAuth auth;
    private DatabaseReference _dbReference;
    private AuthService authService;

    private void Start()
    {
        InitializeFirebase();
    }

    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        _dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        authService = new AuthService(auth, _dbReference);
    }

    public void SwapLoginAndRegister()
    {
        authPanel.SetActive(!authPanel.activeSelf);
        registerPanel.SetActive(!registerPanel.activeSelf);
    }

    public void SignInButton()
    {
        if(!IsEmailValid(emailLoginInput.text))
        {
            PopUpManager.instance.ShowMessage("Email Is Not Valid");
            return;
        }
        if(!IsPasswordValid(passwordLoginInput.text))
        {
            PopUpManager.instance.ShowMessage("Password is too short");
            return;
        }
        authService.SignIn(emailLoginInput.text, passwordLoginInput.text);
    }

    public void SignUpButton()
    {
        if (!IsUsernameValid(usernameRegisterInput.text))
        {
            PopUpManager.instance.ShowMessage("Username is not valid");
            return;
        }
        if (!IsEmailValid(emailRegisterInput.text))
        {
            PopUpManager.instance.ShowMessage("Email Is Not Valid");
            return;
        }
        if(!IsPasswordValid(passwordRegisterInput.text))
        {
            PopUpManager.instance.ShowMessage("Password Is Not Valid");
            return;
        }
        if(!IsPasswordsMatch(passwordRegisterInput.text, passwordRepeatRegisterInput.text))
        {
            PopUpManager.instance.ShowMessage("Password mismatch");
            return;
        }

        authService.SignUp(usernameRegisterInput.text, emailRegisterInput.text, passwordRegisterInput.text);
    }

    private bool IsUsernameValid(string username)
    {
        if (!string.IsNullOrEmpty(username) && username.Length >= 5)
        {
            return true;
        }
        return false;
    }

    private bool IsEmailValid(string email)
    {
        if(!string.IsNullOrEmpty(email))
        {
            return Regex.IsMatch(email, Constants.EMAIL_PATTERN);
        } else
        {
            return false;
        }
    }

    private bool IsPasswordValid(string password)
    {
        if (!string.IsNullOrEmpty(password) && password.Length >= 6)
        {
            return true;
        }
        return false;
    }

    private bool IsPasswordsMatch(string password, string repeatPassword)
    {
        if(string.Equals(password, repeatPassword))
        {
            return true;
        }
        return false;
    }

    private void OnDestroy()
    {
        auth = null;
    }
}
