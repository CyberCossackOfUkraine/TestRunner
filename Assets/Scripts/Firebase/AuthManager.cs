using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Firebase.Database;

public class AuthManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _authPanel;
    [SerializeField] private GameObject _registerPanel;

    [Header("Login")]
    [SerializeField] private InputField _emailLoginInput;
    [SerializeField] private InputField _passwordLoginInput;
    [SerializeField] private Toggle _rememberMeToggle;

    [Header("Register")]
    [SerializeField] private InputField _usernameRegisterInput;
    [SerializeField] private InputField _emailRegisterInput;
    [SerializeField] private InputField _passwordRegisterInput;
    [SerializeField] private InputField _passwordRepeatRegisterInput;

    [Header("Valid Settings")]
    [SerializeField] private int _minUsernameLength;
    [SerializeField] private int _maxUsernameLength;
    [Space]
    [SerializeField] private int _minPasswordLength;

    private FirebaseAuth _auth;
    private DatabaseReference _dbReference;
    private AuthService _authService;

    private void Start()
    {
        InitializeFirebase();
        HandleRememberMe();
    }

    private void HandleRememberMe()
    {
        if (PlayerPrefs.GetInt(Constants.IS_REMEMBERED_KEY) == 1)
        {
            _rememberMeToggle.isOn = true;
            _emailLoginInput.text = PlayerPrefs.GetString(Constants.EMAIL_KEY);
            Debug.Log(PlayerPrefs.GetString(Constants.EMAIL_KEY));

            _passwordLoginInput.text = PlayerPrefs.GetString(Constants.PASSWORD_KEY);
        } else
        {
            _rememberMeToggle.isOn = false;
        }
    }

    public void RememberMeToggle()
    {
        _rememberMeToggle.isOn = !_rememberMeToggle.isOn;
    }

    private void InitializeFirebase()
    {
        _auth = FirebaseAuth.DefaultInstance;
        _dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        _authService = new AuthService(_auth, _dbReference);
    }

    public void SwapLoginAndRegister()
    {
        _authPanel.SetActive(!_authPanel.activeSelf);
        _registerPanel.SetActive(!_registerPanel.activeSelf);
    }

    public void SignInButton()
    {
        if(!IsEmailValid(_emailLoginInput.text))
        {
            PopUpManager.instance.ShowMessage("Email Is Not Valid");
            return;
        }
        if(!IsPasswordValid(_passwordLoginInput.text))
        {
            PopUpManager.instance.ShowMessage("Password is too short");
            return;
        }

        PlayerPrefs.SetInt(Constants.IS_REMEMBERED_KEY, _rememberMeToggle.isOn ? 1 : 0);
        if (_rememberMeToggle.isOn)
        {
            PlayerPrefs.SetString(Constants.EMAIL_KEY, _emailLoginInput.text);
            Debug.Log(_emailLoginInput.text);
            PlayerPrefs.SetString(Constants.PASSWORD_KEY, _passwordLoginInput.text);
        }

        _authService.SignIn(_emailLoginInput.text, _passwordLoginInput.text);
    }

    public void SignUpButton()
    {
        if (!IsUsernameValid(_usernameRegisterInput.text))
        {
            PopUpManager.instance.ShowMessage("Username is not valid");
            return;
        }
        if (!IsEmailValid(_emailRegisterInput.text))
        {
            PopUpManager.instance.ShowMessage("Email Is Not Valid");
            return;
        }
        if(!IsPasswordValid(_passwordRegisterInput.text))
        {
            PopUpManager.instance.ShowMessage("Password Is Not Valid");
            return;
        }
        if(!IsPasswordsMatch(_passwordRegisterInput.text, _passwordRepeatRegisterInput.text))
        {
            PopUpManager.instance.ShowMessage("Password mismatch");
            return;
        }
        _authService.SignUp(_usernameRegisterInput.text, _emailRegisterInput.text, _passwordRegisterInput.text);
    }

    private bool IsUsernameValid(string username)
    {
        if (!string.IsNullOrEmpty(username) && username.Length >= _minUsernameLength && username.Length <= _maxUsernameLength)
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
        if (!string.IsNullOrEmpty(password) && password.Length >= _minPasswordLength)
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
        _auth = null;
    }
}
