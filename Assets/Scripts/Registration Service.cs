using TMPro;
using UnityEngine;
using UnityEngine.Playables;

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

    public bool CheckForEmpty(TMP_InputField field, string notification = "Field is empty")
    {
        if (field.text == string.Empty)
        {
            Debug.Log(notification);
            return false;
        }
        return true;
    }

    public bool CheckForLength(TMP_InputField field, int min, string notification = "Incorrect length")
    {
        if (field.text.Length < min)
        {
            Debug.Log(notification);
            return true;
        }
        return false;
    }

    public void SignUp()
    {
        if (CheckForEmpty(_signupLoginField, _shortLogin) || CheckForLength(_signupLoginField, 3, _shortLogin))
        {
            return;
        }

        if (CheckForEmpty(_signupPasswordField, _shortPassword) || CheckForLength(_signupLoginField, 8, _shortPassword))
        {
            return;
        }

        if (CheckForEmpty(_signupConfirmPasswordField) || CheckForLength(_signupConfirmPasswordField, 8, _shortPassword))
        {
            return;
        }

        if (_signupConfirmPasswordField != _signupPasswordField)
        {
            Debug.Log(_passwordMatch);
            return;
        }

        //Обращение к базе на проверку аккаунта
    }

    public void Login()
    {
        if (CheckForEmpty(_loginField, _shortLogin) || CheckForLength(_loginField, 3, _shortLogin))
        {
            return;
        }

        if (CheckForEmpty(_passwordField, _shortPassword) || CheckForLength(_passwordField, 8, _shortPassword))
        {
            return;
        }

        //
    }
}