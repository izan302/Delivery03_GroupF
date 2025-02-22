using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    // NOTE: Inventory UI slots support drag&drop,
    // implementing the Unity provided interfaces by events system

    public Image Image;
    public TextMeshProUGUI AmountText;
    public TextMeshProUGUI QuantityText;
    public bool IsSelected = false;
    public GameObject Selected;
    private Canvas _canvas;
    private GraphicRaycaster _raycaster;
    private Transform _parent;
    private ItemBase _item;
    private InventoryUI _inventory;
    private ItemSlot _slot;
    private bool isDragging;

    public void Initialize(ItemSlot slot, InventoryUI inventory)
    {
        Image.sprite = slot.Item.ImageUI;
        Image.SetNativeSize();

        AmountText.text = slot.Amount.ToString();
        AmountText.enabled = slot.Amount > 1;

        QuantityText.text = "";

        _slot = slot;
        _slot.OnSlotSelected += OnSlotSelected;
        _slot.OnSlotDeselected += OnSlotDeselected;
        Selected.GetComponent<RawImage>().color = new Color32(246, 255, 146, 0);

        _item = slot.Item;
        _inventory = inventory;

        isDragging = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        
        _parent = transform.parent;

        // Start moving object from the beginning!
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);
        
        // We need a few references from UI
        if (!_canvas)
        {
            _canvas = GetComponentInParent<Canvas>();
            _raycaster = _canvas.GetComponent<GraphicRaycaster>();
        }
        
        // Change parent of our item to the canvas
        transform.SetParent(_canvas.transform, true);
        
        // And set it as last child to be rendered on top of UI
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Moving object around screen using mouse delta
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        // Find scene objects colliding with mouse point on end dragging
        RaycastHit2D hitData = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

        if (hitData)
        {
            Debug.Log("Drop over object: " + hitData.collider.gameObject.name);

            var consumer = hitData.collider.GetComponent<IConsume>();
            bool consumable = _item is ConsumableItem;

            if ((consumer != null) && consumable)
            {
                (_item as ConsumableItem).Use(consumer);
                _inventory.UseItem(_item);
            }
        }

        // Changing parent back to slot
        transform.SetParent(_parent.transform);

        // And centering item position
        transform.localPosition = Vector3.zero;
    }
    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left) {
            if (!isDragging) {
                if (_slot.IsSelected) {
                _slot.IncrementSelection();
                }else {
                _slot.Select();
                }
            }
        }else if (eventData.button == PointerEventData.InputButton.Right) {
            _slot.DecreaseSelection();
        }else if (eventData.button == PointerEventData.InputButton.Middle) {
            _slot.SelectAll();
        }
        
    }

    //MIRAR ERROR PROFE AYUDA ME MUERO.COM
    public void OnSlotSelected() {
        QuantityText.text = _slot.QuantitySelected.ToString();
        if (Selected == null) {
            this._inventory.SlotPrefab.Selected.GetComponent<RawImage>().color = new Color32(246, 255, 146, 76);
        }else {
            Selected.GetComponent<RawImage>().color = new Color32(246, 255, 146, 76);
        }
        
    }

    public void OnSlotDeselected() {
        QuantityText.text = "";
         if (Selected == null) {
            this._inventory.SlotPrefab.Selected.GetComponent<RawImage>().color = new Color32(246, 255, 146, 0);
        }else {
            Selected.GetComponent<RawImage>().color = new Color32(246, 255, 146, 0);
        }
    }
}
