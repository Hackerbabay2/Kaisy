using UnityEngine;
using Zenject;

public class BusketLoader : MonoBehaviour
{
    [SerializeField] private GameObject _busketProduct;
    [SerializeField] private RectTransform _container;
    [SerializeField] private string _notificationCantOpenBusket = "Корзина пуста. Зарегестрируйтесь или войдите в аккаунт, чтобы иметь возможность добавлять товары в корзину";

    [Inject] private UserData _userData;
    [Inject] private NotificationDisplayer _notificationDisplayer;

    private DiContainer _diContainer;

    [Inject]
    public void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    private void OnEnable()
    {
        UpdateCartProducts();
    }

    public void UpdateCartProducts()
    {
        if (_container.childCount > 0)
        {
            foreach (Transform child in _container)
            {
                Destroy(child.gameObject);
            }
        }

        if (_userData.User.Busket.Count > 0)
        {
            foreach (Product product in _userData.User.Busket)
            {
                GameObject tempBusketProduct = _diContainer.InstantiatePrefab(_busketProduct, _container);
                tempBusketProduct.GetComponent<ProductInitializer>().Initialize(product);
            }
        }
    }
}