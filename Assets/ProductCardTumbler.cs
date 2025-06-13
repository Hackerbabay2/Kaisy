using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProductCardTumbler : MonoBehaviour
{
    [SerializeField] private ProductInitializer _productInitializer;

    [Inject] private ProductCardInitializer _productCardInitializer;

    public void OnProductButtonPressed()
    {
        _productCardInitializer.InitializeProduct(_productInitializer.Product);
    }
}
