using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Items/Potion")]
public class ItemPotion : ConsumableItem
{
    public float LifeRestore;
    HealthManager bar;
    void Awake()
    {
        bar = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();
    }

    public override void Use(IConsume consumer)
    {
        consumer.Use(this);
    }
}
