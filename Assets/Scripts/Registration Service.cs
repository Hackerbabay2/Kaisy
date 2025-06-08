using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Zenject;

public class RegistrationService : MonoBehaviour
{
    [Header("Forms")]
    [SerializeField] private GameObject _loginForm;
    [SerializeField] private GameObject _signUpForm;

    [Header("Login form")]
    [SerializeField] private TMP_InputField _loginField;
    [SerializeField] private TMP_InputField _passwordField;

    [Header("Sign up form")]
    [SerializeField] private TMP_InputField _signupLoginField;
    [SerializeField] private TMP_InputField _signupPasswordField;
    [SerializeField] private TMP_InputField _signupConfirmPasswordField;

    [Header("Notifications")]
    [SerializeField] private string _incorrectPassword;
    [SerializeField] private string _shortPassword;
    [SerializeField] private string _shortLogin;
    [SerializeField] private string _passwordMatch;
    [SerializeField] private string _userExists;
    [SerializeField] private string _successfulSignUp;

    [Inject] private NotificationDisplayer _notificationDisplayer;
    [Inject] private DataBase _dataBase;
    [Inject] private StorageService _storageService;
    [Inject] private UserData _userData;

    private void Start()
    {
        StartCoroutine(_storageService.Load());
    }

    public void ShowLoginForm()
    {
        _loginForm.SetActive(true);
        _signUpForm.SetActive(false);
    }

    public void ShowSignUpForm()
    {
        _signUpForm.SetActive(true);
        _loginForm.SetActive(false);
    }

    public bool CheckForLength(TMP_InputField field, int min, string notification = "Incorrect length")
    {
        if (field.text.Length < min)
        {
            _notificationDisplayer.ShowNotificationSafety(notification);
            return true;
        }
        return false;
    }

    public void SignUp()
    {
        if (CheckForLength(_signupLoginField, 3, _shortLogin))
        {
            return;
        }

        if (CheckForLength(_signupPasswordField, 8, _shortPassword))
        {
            return;
        }

        if (CheckForLength(_signupConfirmPasswordField, 8, _shortPassword))
        {
            return;
        }

        if (_signupConfirmPasswordField.text != _signupPasswordField.text)
        {
            _notificationDisplayer.ShowNotificationSafety(_passwordMatch);
            return;
        }

        if (_dataBase.Data.CheckForExistUser(_signupLoginField.text))
        {
            _notificationDisplayer.ShowNotificationSafety(_userExists);
            return;
        }

        _dataBase.Data.AddUser(_signupLoginField.text, _signupPasswordField.text);
        StartCoroutine(_storageService.Save());
        _notificationDisplayer.ShowNotificationSafety(_successfulSignUp);
    }

    public void Login()
    {
        if (CheckForLength(_loginField, 3, _shortLogin))
        {
            return;
        }

        if (CheckForLength(_passwordField, 8, _shortPassword))
        {
            return;
        }

        User user = _dataBase.Data.GetUser(_loginField.text, _passwordField.text);

        if (user != null)
        {
            _userData.Initialize(user);
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            _notificationDisplayer.ShowNotificationSafety(_incorrectPassword);
        }
    }
}