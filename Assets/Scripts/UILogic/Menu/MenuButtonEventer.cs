using UnityEngine;

public class MenuButtonEventer : MonoBehaviour
{
    [SerializeField] private GameObject _mainForm;
    [SerializeField] private GameObject _catalogForm;
    [SerializeField] private GameObject _busketForm;
    [SerializeField] private GameObject _followedForm;
    [SerializeField] private GameObject _profileForm;

    private void DisableAllForm()
    {
        _busketForm?.SetActive(false);
        _catalogForm?.SetActive(false);
        _followedForm?.SetActive(false);
        _profileForm?.SetActive(false);
        _mainForm?.SetActive(false);
    }

    public void ShowMainForm()
    {
        DisableAllForm();
        _mainForm.SetActive(true);
    }

    public void ShowCatalogForm()
    {
        DisableAllForm();
        _catalogForm.SetActive(true);
    }

    public void ShowBusketForm()
    {
        DisableAllForm();
        _busketForm.SetActive(true);
    }

    public void ShowFollowedForm()
    {
        DisableAllForm();
        _followedForm.SetActive(true);
    }

    public void ShowProfileForm()
    {
        DisableAllForm();
        _profileForm.SetActive(true);
    }
}