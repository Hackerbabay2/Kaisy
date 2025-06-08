using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductDatabase", menuName = "Shop/Product Database")]
public class ProductDatabase : ScriptableObject
{
    public List<Product> products;

    public Product GetProductById(string id)
    {
        return products.Find(p => p.id == id);
    }
}
