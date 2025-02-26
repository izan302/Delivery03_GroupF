using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumableItem : ItemBase
{
    public float LifeRestore;
    public abstract void Use(IConsume consumer);
}
