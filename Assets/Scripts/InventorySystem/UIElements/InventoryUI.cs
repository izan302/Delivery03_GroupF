using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory Inventory;
    public InventorySlotUI SlotPrefab;
    public TMP_Text CoinText;
    List<GameObject> _shownObjects;
    public List<ItemSlot> SelectedSlots { get; private set; } = new List<ItemSlot>();
    void Start()
    {
        FillInventory(Inventory);
        CoinText.text = Inventory.Coin.ToString();
    }

    private void OnEnable()
    {
        Inventory.OnInventoryChange += UpdateInventory;
    }

    private void OnDisable()
    {
        Inventory.OnInventoryChange -= UpdateInventory;
    }

    public void UpdateInventory()
    {
        // Regenerate full inventory on changes
        ClearInventory();
        FillInventory(Inventory);
        CoinText.text = Inventory.Coin.ToString();
    }

    private void ClearInventory()
    {
        if (_shownObjects == null) return;

        foreach (var item in _shownObjects)
        {
            if (item != null) Destroy(item);
        }

        _shownObjects.Clear();
    }

    private void FillInventory(Inventory inventory)
    {
        // Lazy initialization for objects list
        if (_shownObjects == null) _shownObjects = new List<GameObject>();

        if (_shownObjects.Count > 0) ClearInventory();

        for (int i = 0; i < inventory.Length; i++)
        {
            _shownObjects.Add(AddSlot(inventory.GetSlot(i)));
        }
    }

    private GameObject AddSlot(ItemSlot inventorySlot)
    {
        // Add a new visual slot UI in inventory UI, using provided prefab
        var element = GameObject.Instantiate(SlotPrefab, Vector3.zero, Quaternion.identity, transform);

        element.Initialize(inventorySlot, this);

        return element.gameObject;
    }

    public void UseItem(ItemBase item)
    {
        Inventory.RemoveItem(item);
    }
}
