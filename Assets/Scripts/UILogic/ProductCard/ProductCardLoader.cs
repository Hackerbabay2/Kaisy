using DanielLochner.Assets.SimpleScrollSnap;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductCardLoader : MonoBehaviour
{
    [SerializeField] private TMP_Text _titel;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private RectTransform _containter;
    [SerializeField] private GameObject _imagePrefab;

    private Product _product;
    public Product Product => _product;

    private void Start()
    {
    }

    public void Initialize(Product product)
    {
        _product = product;
        _titel.text = product.title;
        _price.text = product.price.ToString();
        _description.text = product.description;

        if (_containter.childCount > 0)
        {
            foreach (Transform child in _containter)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (Sprite image in product.images)
        {
            GameObject imageObject = Instantiate(_imagePrefab, _containter);
            imageObject.GetComponent<Image>().sprite = image;
            imageObject.SetActive(true);
        }
    }
}