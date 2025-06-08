using System.Linq;
using UnityEngine;
using Zenject;

public class FollowedLoader : MonoBehaviour
{
    [SerializeField] private GameObject _followedProduct;
    [SerializeField] private ProductDatabase _productDataBase;
    [SerializeField] private RectTransform _container;
    [SerializeField] private string _notificationCantOpenFollowed = "Избранное пусто пуста. Зарегестрируйтесь или войдите в аккаунт, чтобы иметь возможность добавлять товары в корзину";

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
        UpdateFollowedProducts();
    }

    public void UpdateFollowedProducts()
    {
        if (_userData == null || _userData.User == null)
        {
            _notificationDisplayer.ShowNotificationSafety(_notificationCantOpenFollowed);
            return;
        }

        if (_container.childCount > 0)
        {
            foreach (Transform child in _container)
            {
                Destroy(child.gameObject);
            }
        }

        if (_userData.User.FollowedIds.Count > 0)
        {
            foreach (string product in _userData.User.FollowedIds)
            {
                Product tempProduct = _productDataBase.products.FirstOrDefault(p => p.id.ToString() == product);
                GameObject tempBusketProduct = _diContainer.InstantiatePrefab(_followedProduct, _container);
                tempBusketProduct.GetComponent<ProductInitializer>().Initialize(tempProduct);
            }
        }
    }
}
