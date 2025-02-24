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
    public void UseButton()
    {
        Use(PlayerInventory);
    }

    private void Transaction(Inventory _buyer, Inventory _seller)
    {
        List<ItemSlot> Items = _seller.GetSelectedSlots();
        foreach (var item in Items)
        {
            if (_buyer.Coin - (item.Item.Value * item.QuantitySelected) >= 0)
            {
                _buyer.Coin -= (item.Item.Value * item.QuantitySelected);
                _seller.Coin += (item.Item.Value * item.QuantitySelected);
                
                item.IsSelected = false;
                _buyer.AddItems(item);
                _seller.RemoveItems(item.Item, item.QuantitySelected);

                item.QuantitySelected = 0;
                //MUSICA BIEN (Aldeano minecraft)
            }
            else
            {
                //MUSICA ERROR (Aldeano minecraft)
            }
        }
    }
    private void Use(Inventory user)
    {
        List<ItemSlot> Items = user.GetSelectedSlots();
        foreach (var item in Items)
        {
            if (item.Item.IsConsumable)
            {
                (item.Item as ConsumableItem).Use(item.Item as IConsume);
                item.Use();
            }
        }
    }
}
