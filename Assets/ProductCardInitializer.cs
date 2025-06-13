using System.ComponentModel;
using UnityEngine;
using Zenject;

public class ProductCardInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _productCardPrefab;
    [SerializeField] private GameObject _productPanel;

    private ProductCardLoader _productCardLoader;
    private DiContainer _container;
    private GameObject _productCardActiveObject;

    [Inject]
    public void Construct(DiContainer diContainer)
    {
        _container = diContainer;
    }

    public void InitializeProduct(Product product)
    {
        _productPanel.SetActive(true);
        _productCardActiveObject = _container.InstantiatePrefab(_productCardPrefab, _productPanel.transform);
        _productCardPrefab.SetActive(true);
        _productCardLoader = _productCardActiveObject.GetComponent<ProductCardLoader>();
        _productCardLoader.Initialize(product);
    }

    public void OnEscape()
    {
        _productPanel.SetActive(false);
        Destroy(_productCardActiveObject);
    }
}
