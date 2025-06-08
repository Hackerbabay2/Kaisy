using System.ComponentModel;
using UnityEngine;
using Zenject;

public class MainProductLoader : MonoBehaviour
{
    [SerializeField] private RectTransform _productContainer;
    [SerializeField] private GameObject _productPrefab;
    [SerializeField] private ProductDatabase _productData;

    private DiContainer _diContainer;

    [Inject]
    public void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    private void OnEnable()
    {
        if (_productContainer.childCount > 0)
        {
            foreach (Transform child in _productContainer)
            {
                Destroy(child.gameObject);
            }
        }

        if (_productData.products.Count >= 10)
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject tempProduct =  _diContainer.InstantiatePrefab(_productPrefab, _productContainer);
                tempProduct.GetComponent<ProductInitializer>().Initialize(_productData.products[i]);
            }
        }
        else
        {
            foreach (Product product in _productData.products)
            {
                GameObject tempProduct = _diContainer.InstantiatePrefab(_productPrefab, _productContainer);
                tempProduct.GetComponent<ProductInitializer>().Initialize(product);
            }
        }
    }
}