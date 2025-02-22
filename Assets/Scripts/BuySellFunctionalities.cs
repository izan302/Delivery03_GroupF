using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuySellFunctionalities : MonoBehaviour
{
    public Inventory PlayerInventory;
    public Inventory ShopkeeperInventory;

    public void BuyButton()
    {
        Transaction(PlayerInventory, ShopkeeperInventory);
    }

    public void SellButton()
    {
        Transaction(ShopkeeperInventory, PlayerInventory);
    }

    private void Transaction(Inventory _buyer, Inventory _seller)
    {
        List<ItemSlot> Items = _seller.GetSelectedSlots();
        foreach (var item in Items)
        {
            if (_buyer.Coin - (item.Item.Value * item.QuantitySelected) >= 0)
            {
                item.IsSelected = false;
                _buyer.AddItems(item);
                _seller.RemoveItems(item.Item, item.QuantitySelected);

                _buyer.Coin -= item.Item.Value * item.QuantitySelected;
                _seller.Coin += item.Item.Value * item.QuantitySelected;
                item.QuantitySelected = 0;
            }
            else
            {

            }
        }
    }
}
