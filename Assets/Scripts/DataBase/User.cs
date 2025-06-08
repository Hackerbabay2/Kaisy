using System;
using System.Collections.Generic;

[Serializable]
public class User
{
    public string Login;
    public string Password;
    public string AvatarImagePath;
    public List<Product> Busket;

    public User(string login, string password)
    {
        Login = login;
        Password = password;
        Busket = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null");
        }
        Busket.Add(product);
    }

    internal void RemoveProduct(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null");
        }
        if (Busket.Contains(product))
        {
            Busket.Remove(product);
        }
        else
        {
            throw new InvalidOperationException("Product not found in the busket");
        }
    }
}