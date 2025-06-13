using TMPro;
using UnityEngine;
using Zenject;
using System.Linq;
using System.Text.RegularExpressions;
using System;
using Unity.VisualScripting;

public class CatalogDropDownLogic : MonoBehaviour
{
    [SerializeField] private GameObject _product;
    [SerializeField] private ProductDatabase _productDataBase;
    [SerializeField] private RectTransform _container;
    [SerializeField] private TMP_Dropdown _dropdown;

    private DiContainer _diContainer;

    [Inject]
    public void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    private void Start()
    {
        _dropdown.onValueChanged.AddListener(LoadProductByCategories);
        LoadProductByCategories(_dropdown.value);
    }

    public int LevenshteinDistance(string source, string target)
    {
        if (String.IsNullOrEmpty(source))
        {
            if (String.IsNullOrEmpty(target)) return 0;
            return target.Length;
        }
        if (String.IsNullOrEmpty(target)) return source.Length;

        var m = target.Length;
        var n = source.Length;
        var distance = new int[2, m + 1];
        for (var j = 1; j <= m; j++) distance[0, j] = j;

        var currentRow = 0;
        for (var i = 1; i <= n; ++i)
        {
            currentRow = i & 1;
            distance[currentRow, 0] = i;
            var previousRow = currentRow ^ 1;
            for (var j = 1; j <= m; j++)
            {
                var cost = (target[j - 1] == source[i - 1] ? 0 : 1);
                distance[currentRow, j] = Math.Min(Math.Min(
                            distance[previousRow, j] + 1,
                            distance[currentRow, j - 1] + 1),
                            distance[previousRow, j - 1] + cost);
            }
        }
        return distance[currentRow, m];
    }

    private void LoadProductByCategories(int selectedIndex)
    {
        if (_container.childCount > 0)
        {
            foreach (Transform child in _container)
            {
                Destroy(child.gameObject);
            }
        }

        if (selectedIndex <= 0)
        {
            foreach (Product product in _productDataBase.products)
            {
                GameObject tempProduct = _diContainer.InstantiatePrefab(_product, _container);
                tempProduct.GetComponent<ProductInitializer>().Initialize(product);
            }
        }
        else
        {
            string selectedCategory = _dropdown.options[_dropdown.value].text;

            foreach (Product product in _productDataBase.products)
            {
                int distance = LevenshteinDistance(selectedCategory, product.title.Split(" ")[0]);
                int threshold = Math.Max(3, selectedCategory.Length / 4);

                if (distance <= threshold)
                {
                    GameObject tempProduct = _diContainer.InstantiatePrefab(_product, _container);
                    tempProduct.GetComponent<ProductInitializer>().Initialize(product);
                }
            }
        }
    }

    private void OnDestroy()
    {
        _dropdown.onValueChanged.RemoveListener(LoadProductByCategories);
    }
}