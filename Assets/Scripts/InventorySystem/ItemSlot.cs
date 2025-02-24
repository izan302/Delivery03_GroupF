using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

[System.Serializable]
public class ItemSlot
{
    public ItemBase Item;
    public int Amount;
    public bool IsSelected;
    public Action OnSlotSelected;
    public Action OnSlotDeselected;
    public int QuantitySelected;
    public ItemSlot(ItemBase item)
    {
        this.Item = item;
        Amount = 1;
        IsSelected = false;
        QuantitySelected = 0;
    }

    internal bool HasItem(ItemBase item)
    {
        return (item == Item);
    }

    internal bool CanHold(ItemBase item)
    {
        if (item.IsStackable) return (item == Item);

        return false;
    }

    internal void AddOne()
    {
        Amount++;
    }

    internal void RemoveOne()
    {
        Amount--;
    }

    internal void RemoveByQuantity(int Quantity)
    {
        Amount -= Quantity;
    }

    public bool IsEmpty()
    {
        return Amount < 1;
    }

    public void Select()
    {
        if (!IsSelected)
        {
            IsSelected = true;
            QuantitySelected = 1;
            OnSlotSelected?.Invoke();
        }
    }

    public void IncrementSelection()
    {
        if (IsSelected)
        {
            if (QuantitySelected < Amount)
            {
                QuantitySelected++;
                OnSlotSelected?.Invoke();
            }
            else
            {
                Deselect();
            }
        }
    }
    public void SelectAll()
    {
        QuantitySelected = Amount;
        OnSlotSelected?.Invoke();
    }
    public void DecreaseSelection()
    {
        if (QuantitySelected == 1)
        {
            Deselect();
        }
        else
        {
            QuantitySelected--;
            OnSlotSelected?.Invoke();
        }
    }
    public void Deselect()
    {
        if (IsSelected)
        {
            IsSelected = false;
            QuantitySelected = 0;
            OnSlotDeselected?.Invoke();
        }
    }
    public void Use()
    {
        RemoveOne();
    }
}


