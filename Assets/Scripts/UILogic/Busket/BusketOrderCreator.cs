using UnityEngine;
using Zenject;

public class BusketOrderCreator : MonoBehaviour
{
    [SerializeField] private GameObject _qrForm;
    [SerializeField] private string _notificationCantCreateOrder = "����������������� ��� �������, ����� ����� ����������� �������� �����";
    [SerializeField] private string _notificationEmptyCart = "���� ������� �����, �������� ������ � �������, ����� �������� �����";
    [SerializeField] private string _notificationCantCreateOrderWithoutAddress = "����������, ������� ����� � �������� ������ ��� ��������";

    [Inject] private UserData _userData;
    [Inject] private StorageService _storageService;
    [Inject] NotificationDisplayer _notificationDisplayer;
    [Inject] private BusketLoader _busketLoader;

    public void OnOrderPressed()
    {
        if (_userData == null || _userData.User == null)
        {
            _notificationDisplayer.ShowNotificationSafety(_notificationCantCreateOrder);
            return;
        }

        if (_userData.User.BusketIds.Count > 0)
        {
            if (string.IsNullOrEmpty(_userData.User.PostalCode) == false && string.IsNullOrEmpty(_userData.User.Adress) == false)
            {
                _qrForm.SetActive(true);
                _userData.User.AddOrder();
                _busketLoader.UpdateCartProducts();
                StartCoroutine(_storageService.Save());
            }
            else
            {
                _notificationDisplayer.ShowNotificationSafety(_notificationCantCreateOrderWithoutAddress);
            }
        }
        else
        {
            _notificationDisplayer.ShowNotificationSafety(_notificationEmptyCart);
        }
    }
}