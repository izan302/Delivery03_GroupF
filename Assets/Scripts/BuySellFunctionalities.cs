using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuySellFunctionalities : MonoBehaviour
{
    public Inventory PlayerInventory;
    public Inventory ShopkeeperInventory;
    public GameObject Healthbar;
    AudioManager Audio;

    public void Awake()
    {
        Audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
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
        List<ItemSlot> Items = PlayerInventory.GetSelectedSlots();
        foreach (var item in Items)
        {
            if (item.Item.IsConsumable)
            {
                for (int i = 0; i < item.QuantitySelected; i++)
                {
                    if (Healthbar.GetComponent<HealthManager>().GetCurrentHealth() != Healthbar.GetComponent<HealthManager>().GetMaxHealth())
                    {
                        (item.Item as ConsumableItem).Use(Healthbar.GetComponent<IConsume>());
                        item.RemoveOne();
                    }
                }
            }
            item.Deselect();
        }
        PlayerInventory.UpdateInventory();
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

                Audio.PlaySFX(Audio.VillagerGood);
                //MUSICA BIEN (Aldeano minecraft)
            }
            else
            {
                Audio.PlaySFX(Audio.VillagerConfused);
                //MUSICA ERROR (Aldeano minecraft)
            }
        }
    }
}
