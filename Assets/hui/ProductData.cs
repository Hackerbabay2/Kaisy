using UnityEngine;

[CreateAssetMenu(fileName = "New Product", menuName = "Shop/Product")]
public class ProductData : ScriptableObject
{
    public string ProductName;
    public string ProductPrice;
    public Sprite ProductImage;
    public bool IsFavorite;
}
