using UnityEngine;
using Zenject;

public class ProductBusketModifier : MonoBehaviour
{
    [SerializeField] private ProductInitializer _productInitializer;
    [SerializeField] private string _notificationCantAddProduct = "����������������� ��� �������, ����� ����� ����������� �������� ����� � �������";

    [Inject] private BusketLoader _busketLoader;
    [Inject] private UserData _userData;
    [Inject] private NotificationDisplayer _notificationDisplayer;

    public void AddProduct()
    {
        if (_userData == null || _userData.User == null)
        {
            _notificationDisplayer.ShowNotificationSafety(_notificationCantAddProduct);
            return;
        }
        _userData.User.AddProduct(_productInitializer.Product);
    }

    public void RemoveProduct()
    {
        if (_userData == null || _userData.User == null)
        {
            return;
        }
        _userData.User.RemoveProduct(_productInitializer.Product);
        _busketLoader.UpdateCartProducts();
    }
}
