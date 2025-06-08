using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class BusketLoader : MonoBehaviour
{
    [SerializeField] private GameObject _busketProduct;
    [SerializeField] private ProductDatabase _productDataBase;
    [SerializeField] private RectTransform _container;
    [SerializeField] private TMP_Text _cartCost;
    [SerializeField] private string _notificationCantOpenBusket = "Корзина пуста. Зарегестрируйтесь или войдите в аккаунт, чтобы иметь возможность добавлять товары в корзину";

    [Inject] private UserData _userData;
    [Inject] private NotificationDisplayer _notificationDisplayer;

    private DiContainer _diContainer;
    private float _cartCostValue;

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
        if (_userData == null || _userData.User == null)
        {
            _notificationDisplayer.ShowNotificationSafety(_notificationCantOpenBusket);
            return;
        }

        if (_container.childCount > 0)
        {
            foreach (Transform child in _container)
            {
                Destroy(child.gameObject);
            }
        }

        if (_userData.User.BusketIds.Count > 0)
        {
            _cartCostValue = 0;

            foreach (string product in _userData.User.BusketIds)
            {
                Product tempProduct = _productDataBase.products.FirstOrDefault(p => p.id.ToString() == product);
                _cartCostValue += tempProduct.price;
                GameObject tempBusketProduct = _diContainer.InstantiatePrefab(_busketProduct, _container);
                tempBusketProduct.GetComponent<ProductInitializer>().Initialize(tempProduct);
            }
            _cartCost.text = $"Стоимость корзины: {_cartCostValue} руб.";
        }
    }
}