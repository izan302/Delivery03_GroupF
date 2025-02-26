﻿using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Items/Potion")]
public class ItemPotion : ConsumableItem
{
    public override void Use(IConsume consumer)
    {
        consumer.Use(this);
    }
}
