using System;
using System.Collections.Generic;

[Serializable]
public class Order
{
    public string OrderNumber;
    public List<string> ProductIds;

    public Order(string orderNumber, List<string> productsIds)
    {
        ProductIds = new List<string>();
        OrderNumber = orderNumber;
        ProductIds = productsIds;
    }

    public void InitializeOrder(string orderNumber, List<string> productsIds)
    {
        OrderNumber = orderNumber;
        ProductIds = productsIds;
    }
}