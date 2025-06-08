using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class RegistrationSceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject _registrationButton;
    [SerializeField] private TMP_Text _dataText;
    [SerializeField] private TMP_InputField _emailField;
    [SerializeField] private TMP_InputField _postalCode;
    [SerializeField] private AvatarChanger _avatarChanger;
    [SerializeField] private string _notificationSuccessfullConfirm = "Данные сохраненны!";
    [SerializeField] private string _notificationNeedAuthorizatoin = "Пожалуйста, авторизируйтесь, чтобы иметь возможность внести данные";
    [SerializeField] private string _notificationNeedEmailAndPostalCode = "Пожалуйста, укажите почтовый индекс и адрес для доставки";

    [Inject] private UserData _userData;
    [Inject] private StorageService _storageService;
    [Inject] private NotificationDisplayer _notificationDisplayer;

    private void OnEnable()
    {
        if (_userData == null || _userData.User == null)
        {
            _registrationButton.SetActive(true);
        }
        else
        {
            _registrationButton.SetActive(false);
            _dataText.text = $"Добро пожаловать, {_userData.User.Login}!\n";

            if (string.IsNullOrEmpty(_userData.User.AvatarImagePath) == false)
            {
                _avatarChanger.LoadAvatarAsPosible();
            }

            if (string.IsNullOrEmpty(_userData.User.Adress) == false)
            {
                _emailField.text = _userData.User.Adress;
            }

            if (string.IsNullOrEmpty(_userData.User.PostalCode) == false)
            {
                _postalCode.text = _userData.User.PostalCode;
            }
        }
    }

    public void OnRegistrationButtonClick()
    {
        SceneManager.LoadScene("RegistrationScene");
    }

    public void OnAdditionalInformationPressed()
    {
        if (_userData == null || _userData.User == null)
        {
            _notificationDisplayer.ShowNotificationSafety(_notificationNeedAuthorizatoin);
            return;
        }

        if (string.IsNullOrEmpty(_emailField.text) || string.IsNullOrEmpty(_postalCode.text))
        {
            _notificationDisplayer.ShowNotificationSafety(_notificationNeedEmailAndPostalCode);
            return;
        }

        _userData.User.InitializeAdress(_emailField.text);
        _userData.User.InitializePostalCode(_postalCode.text);

        StartCoroutine(_storageService.Save());

        _notificationDisplayer.ShowNotificationSafety(_notificationSuccessfullConfirm);
    }
}