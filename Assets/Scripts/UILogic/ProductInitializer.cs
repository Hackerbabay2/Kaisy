using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductInitializer : MonoBehaviour
{
    [SerializeField] private TMP_Text _titel;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private RectTransform _containter;
    [SerializeField] private GameObject _imagePrefab;

    private Product _product;

    public Product Product => _product;

    public void Initialize(Product product)
    {
        _product = product;
        _titel.text = product.title;
        _price.text = product.price.ToString();

        foreach (Sprite image in product.images)
        {
            GameObject imageObject = Instantiate(_imagePrefab, _containter);
            imageObject.GetComponent<Image>().sprite = image;
            imageObject.SetActive(true);
        }
    }
}
