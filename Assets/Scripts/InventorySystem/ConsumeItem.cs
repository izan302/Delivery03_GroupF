using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeItem : MonoBehaviour, IConsume
{
    HealthManager bar;
    void Awake()
    {
        bar = GetComponent<HealthManager>();
    }
    public void Use(ConsumableItem item)
    {
        if (item is ItemPotion)
        {
            //Debug.Log("Health potion consumed!");
            bar.HealTaken((item as ItemPotion).LifeRestore);
        }
    }
}
