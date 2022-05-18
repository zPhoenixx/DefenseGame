using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Item[] inventory = new Item[6];

    private PlayerUIManager UIManager;
    private PlayerStats stats;

    public float itemsAttackDamage = 0;
    public float itemsMagicDamage = 0;
    public float itemsMaxHealth = 0;
    public float itemsMaxMana = 0;
    public float itemsManaRegen = 0;
    public float itemsHealthRegen = 0;
    public float itemsArmor = 0;
    public float itemsAttackRange = 0;
    public float itemsAttackSpeed = 0;
    public float itemsMovementSpeed = 0;

    private void Start()
    {
        stats = GetComponent<PlayerStats>();
        UIManager = GameManager.instance.GetComponent<PlayerUIManager>();
    }
    public bool AddItem(Item _item)
    {
        for (int i = 0; i < 6; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = _item;
                UpdateItemStats();
                UIManager.UpdateInventory(inventory);
                return true;
            }
        }
        return false;
    }
    public void RemoveItem(int slot)
    {
        inventory[slot] = null;
        UpdateItemStats();
        UIManager.UpdateInventory(inventory);
    }
    public void SwapItems(int slot1, int slot2)
    {
        if (inventory[slot1 - 1] == null)
            return;
        Item tempItem = inventory[slot1 - 1];
        inventory[slot1 - 1] = inventory[slot2 - 1];
        inventory[slot2 - 1] = tempItem;
        UIManager.UpdateInventory(inventory);
    }
    public void UpdateItemStats()
    {
        itemsAttackDamage = 0;
        itemsMagicDamage = 0;
        itemsMaxHealth = 0;
        itemsMaxMana = 0;
        itemsManaRegen = 0;
        itemsHealthRegen = 0;
        itemsArmor = 0;
        itemsAttackRange = 0;
        itemsAttackSpeed = 0;
        itemsMovementSpeed = 0;
        for (int i = 0; i < 6; i++)
        {
            if (inventory[i] != null)
            {
                itemsAttackDamage += inventory[i].attackDamage;
                itemsMagicDamage += inventory[i].magicDamage;
                itemsMaxHealth += inventory[i].maxHealth;
                itemsMaxMana += inventory[i].maxMana;
                itemsManaRegen += inventory[i].manaRegen;
                itemsHealthRegen += inventory[i].healthRegen;
                itemsArmor += inventory[i].armor;
                itemsAttackRange += inventory[i].attackRange;
                itemsAttackSpeed += inventory[i].attackSpeed;
                itemsMovementSpeed += inventory[i].movementSpeed;
            }
        }
        stats.UpdateStats();
    }
}
