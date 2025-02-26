using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "NewInventory", menuName = "Inventory System/Inventory")]
public class Inventory : ScriptableObject
{
    // NOTE: One slot can contain multiple items of one type

    [SerializeField]
    List<ItemSlot> Slots;
    public int Length => Slots.Count;
    public Action OnInventoryChange;
    public int Coin = 100;
    AudioManager Audio;
    public void OnEnable()
    {
        Audio = GameObject.FindAnyObjectByType<AudioManager>();
    }
    public void AddItem(ItemBase item)
    {
        // Lazy initialization of slots list
        if (Slots == null) Slots = new List<ItemSlot>();

        var slot = GetSlot(item);

        if ((slot != null) && (item.IsStackable))
        {
            slot.AddOne();
        }
        else
        {
            slot = new ItemSlot(item);
            Slots.Add(slot);
        }

        OnInventoryChange?.Invoke();
    }

    public void RemoveItem(ItemBase item)
    {
        if (Slots == null) return;

        var slot = GetSlot(item);

        if (slot != null)
        {
            slot.RemoveOne();
            if (slot.IsEmpty()) RemoveSlot(slot);
        }

        OnInventoryChange?.Invoke();
    }

    private void RemoveSlot(ItemSlot slot)
    {
        Slots.Remove(slot);
    }

    private ItemSlot GetSlot(ItemBase item)
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].HasItem(item)) return Slots[i];
        }

        return null;
    }

    public ItemSlot GetSlot(int i)
    {
        return Slots[i];
    }
    public void AddItems(ItemSlot _slot)
    {

        var slot = GetSlot(_slot.Item);

        if ((slot != null) && (slot.Item.IsStackable))
        {
            if (_slot.QuantitySelected != 0)
            {
                slot.Amount += _slot.QuantitySelected;
            }
            else
            {
                slot.Amount++;
            }

        }
        else
        {
            slot = new ItemSlot(_slot.Item);
            Slots.Add(slot);
        }

        OnInventoryChange?.Invoke();
    }

    public void RemoveItems(ItemBase item, int Quantity)
    {
        if (Slots == null) return;

        var slot = GetSlot(item);

        if (slot != null)
        {
            slot.RemoveByQuantity(Quantity);
            if (slot.IsEmpty()) RemoveSlot(slot);
        }

        OnInventoryChange?.Invoke();
    }
    public List<ItemSlot> GetSelectedSlots()
    {
        List<ItemSlot> SelectedSlots = new List<ItemSlot>();
        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].IsSelected)
            {
                SelectedSlots.Add(Slots[i]);
            }
        }
        return SelectedSlots;
    }
    public bool BuyItem(ItemSlot _slot)
    {
        bool ItemSold = false;
        if (_slot.QuantitySelected != 0)
        {
            if (Coin - _slot.Item.Value * _slot.QuantitySelected >= 0)
            {
                ItemSold = true;
                Coin -= _slot.Item.Value * _slot.QuantitySelected;
                AddItems(_slot);
            }
        }
        else
        {
            if (Coin - _slot.Item.Value >= 0)
            {
                ItemSold = true;
                Coin -= _slot.Item.Value;
                AddItems(_slot);
            }
        }

        return ItemSold;
    }

    public void SellItem(Inventory _buyer, ItemSlot _slot)
    {
        if (_buyer.BuyItem(_slot))
        {
            if (_slot.QuantitySelected == 0)
            {
                Coin += _slot.Item.Value;
                RemoveItems(_slot.Item, 1);
            }
            else
            {
                Coin += _slot.Item.Value * _slot.QuantitySelected;
                RemoveItems(_slot.Item, _slot.QuantitySelected);
            }
            Audio.PlaySFX(Audio.VillagerGood);
        }
        else
        {
            Audio.PlaySFX(Audio.VillagerConfused);
        }
    }
    public void UpdateInventory()
    {
        OnInventoryChange?.Invoke();
    }
}
