using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public ItemType ItemType { get; private set; }
    public int Price { get; private set; }
    public int Point { get; private set; }
    public string ItemName { get; private set; }
    
    public void SetItem(int price, string name, int point, ItemType type)
    { 
        Price = price;
        ItemType = type;
        ItemName = name;
        Point = point;
    }




}
