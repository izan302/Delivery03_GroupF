using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    public Inventory Inventory;   
    public void PickUp(ICanBePicked item)
    {
        // Add picked up item to shop inventory
        Inventory.AddItem(item.GetItem());
    }    
}
