using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Product
{
    public string title; // название
    public string description; // описание 
    public List<Sprite> images; // список изображений
    public float price; // цена
    public string id;
}
