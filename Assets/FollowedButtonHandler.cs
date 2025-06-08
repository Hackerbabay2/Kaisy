using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class FollowedButtonHandler : MonoBehaviour
{
    [SerializeField] private Sprite _emptyHeart;
    [SerializeField] private Sprite _filledHeart;
    [SerializeField] private Image _heartImage;
    [SerializeField] private FollowedModifier _FollowedModifier;
    [SerializeField] private ProductInitializer _productInitializer;

    [Inject] private UserData _userData;

    public void UpdateHeartState()
    {
        if (_userData == null || _userData.User == null)
        {
            return;
        }

        if (_userData.User.FollowedIds.Contains(_productInitializer.Product.id))
        {
            _heartImage.sprite = _filledHeart;
        }
        else
        {
            _heartImage.sprite = _emptyHeart;
        }
    }

    public void OnHeartPressed()
    {
        if (_userData == null || _userData.User == null)
        {
            return;
        }

        if (_heartImage.sprite == _emptyHeart)
        {
            _FollowedModifier.AddFollowed();
            _heartImage.sprite = _filledHeart;
        }
        else
        {
            _FollowedModifier.RemoveFollowed();
            _heartImage.sprite = _emptyHeart;
        }
    }
}