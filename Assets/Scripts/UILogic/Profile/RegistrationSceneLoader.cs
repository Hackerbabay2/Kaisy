using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class RegistrationSceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject _registrationButton;
    [SerializeField] private TMP_Text _dataText;

    [Inject] private UserData _userData;

    private void OnEnable()
    {
        if (_userData == null || _userData.User == null)
        {
            _registrationButton.SetActive(true);
            _dataText.text = "No user data available. Please register.";
        }
        else
        {
            _registrationButton.SetActive(false);
            _dataText.text = $"Welcome, {_userData.User.Login}!\n";
        }
    }

    public void OnRegistrationButtonClick()
    {
        SceneManager.LoadScene("RegistrationScene");
    }
}