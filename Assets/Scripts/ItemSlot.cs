using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler , IDropHandler
{
    public int slotNumber;
    public Image slotImage;
    public Sprite emptySprite;
    private Item item;
    private PlayerInventory inventory;
    private InfoBoxUIManager infoManager;
    private CanvasGroup canvasGroup;
    void Start()
    {
        inventory = FindObjectOfType<PlayerInventory>().GetComponent<PlayerInventory>();
        infoManager = GameManager.instance.GetComponent<InfoBoxUIManager>();
        canvasGroup = GetComponentInChildren<CanvasGroup>();
    }
    public void AddItem(Item _item)
    {
        item = _item;
        slotImage.sprite = _item.itemSprite;
    }
    public void RemoveItem()
    {
        item = null;
        slotImage.sprite = emptySprite;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
            infoManager.ShowInfoBox(ItemDescription());
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        infoManager.HideInfoBox();
    }
    private string ItemDescription()
    {
        if (item == null)
            return "";
        string description = "";

        description += item.itemName + "\n";
        if (item.attackDamage != 0)
            description += "Attack Damage +" + item.attackDamage + "\n";
        if (item.magicDamage != 0)
            description += "Magic Damage +" + item.magicDamage + "\n";
        if (item.maxHealth != 0)
            description += "Max Health +" + item.maxHealth + "\n";
        if (item.maxMana != 0)
            description += "Max Mana +" + item.maxMana + "\n";
        if (item.manaRegen != 0)
            description += "Mana Regen +" + item.manaRegen + "\n";
        if (item.healthRegen != 0)
            description += "Health Regen +" + item.healthRegen + "\n";
        if (item.armor != 0)
            description += "Armor +" + item.armor + "\n";
        if (item.attackRange != 0)
            description += "Attack Range +" + item.attackRange + "\n";
        if (item.attackSpeed != 0)
            description += "Attack Speed +" + item.attackSpeed + "\n";
        if (item.movementSpeed != 0)
            description += "Movement Speed +" + item.movementSpeed + "\n";

        return description;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
            canvasGroup.alpha = .6f;
            canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            slotImage.transform.position = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            slotImage.transform.position = Input.mousePosition;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            int slot = eventData.pointerDrag.GetComponentInParent<ItemSlot>().slotNumber;
            if (slot != slotNumber)
                inventory.SwapItems(slot, slotNumber);
        }
    }
}
