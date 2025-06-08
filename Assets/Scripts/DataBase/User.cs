using System;
using System.Collections.Generic;

[Serializable]
public class User
{
    public string Login;
    public string Password;
    public string AvatarImagePath;
    public string Adress;
    public string PostalCode;
    public List<string> BusketIds;
    public List<string> FollowedIds;
    public List<Order> Orders;

    public User()
    {
        BusketIds = new List<string>();
        FollowedIds = new List<string>();
        Orders = new List<Order>();
    }

    public User(string login, string password)
    {
        Login = login;
        Password = password;
        BusketIds = new List<string>();
        FollowedIds = new List<string>();
        Orders = new List<Order>();
    }

    public void AddFollowed(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null");
        }
        if (!FollowedIds.Contains(product.id))
        {
            FollowedIds.Add(product.id);
        }
    }

    public void RemoveFollowed(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null");
        }
        if (FollowedIds.Contains(product.id))
        {
            FollowedIds.Remove(product.id);
        }
    }

    public void AddProduct(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null");
        }
        BusketIds.Add(product.id);
    }

    public void RemoveProduct(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null");
        }
        if (BusketIds.Contains(product.id))
        {
            BusketIds.Remove(product.id);
        }
        else
        {
            throw new InvalidOperationException("Product not found in the busket");
        }
    }

    public void InitializePostalCode(string postalCode)
    {
        PostalCode = postalCode;
    }

    public void InitializeAdress(string adress)
    {
        Adress = adress;
    }

    public void InitializeAvatar(string path)
    {
        AvatarImagePath = path;
    }

    public void AddOrder()
    {
        string orderNumber = Guid.NewGuid().GetHashCode().ToString();
        Orders.Add(new Order(orderNumber, new List<string>(BusketIds)));
        BusketIds.Clear();
    }
}