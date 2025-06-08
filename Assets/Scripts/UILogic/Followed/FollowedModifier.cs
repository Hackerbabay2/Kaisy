using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FollowedModifier : MonoBehaviour
{
    [SerializeField] private ProductInitializer _productInitializer;
    [SerializeField] private string _notificationCantAddProduct = "«арегестрируйтесь или войдите, чтобы иметь возможность добавить товар в избранное";

    [Inject] private FollowedLoader _followedLoader;
    [Inject] private UserData _userData;
    [Inject] private NotificationDisplayer _notificationDisplayer;
    [Inject] private StorageService _storageService;

    public void AddFollowed()
    {
        if (_userData == null || _userData.User == null)
        {
            _notificationDisplayer.ShowNotificationSafety(_notificationCantAddProduct);
            return;
        }
        _userData.User.AddFollowed(_productInitializer.Product);
        StartCoroutine(_storageService.Save());
    }

    public void RemoveFollowed()
    {
        if (_userData == null || _userData.User == null)
        {
            return;
        }
        _userData.User.RemoveFollowed(_productInitializer.Product);
        _followedLoader.UpdateFollowedProducts();
        StartCoroutine(_storageService.Save());
    }
}