using System;
using System.Collections;
using System.Collections.Generic;
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
        Amount-=Quantity;
    }

    public bool IsEmpty()
    {
        return Amount < 1;
    }

    public void Select()
    {
        if (!IsSelected) {
            IsSelected = true;
            QuantitySelected = 1;
            OnSlotSelected?.Invoke();
        }
        
        /*
        if (IsSelected) {
            if (QuantitySelected != Amount) {
                QuantitySelected++;
            }
            else {
                Deselect();
            }            
        }else {
            IsSelected = !IsSelected;
            QuantitySelected = 1;
        }
        
        if (IsSelected) {
            OnSlotSelected?.Invoke();
        }else {
            OnSlotDeselected?.Invoke();
        }*/
    }
    
    public void IncrementSelection() {
        if (IsSelected) {
            if (QuantitySelected < Amount) {
                QuantitySelected++;
                OnSlotSelected?.Invoke();
            }else {
                Deselect();
            }
        }
    }
    public void Deselect() {
        if (IsSelected) {
            IsSelected = false;
            QuantitySelected = 0;
            OnSlotDeselected?.Invoke();
        }
    }
    
}


