using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "";

    public float attackDamage = 0;
    public float magicDamage = 0;
    public float maxHealth = 0;
    public float maxMana = 0;
    public float manaRegen = 0;
    public float healthRegen = 0;
    public float armor = 0;
    public float attackRange = 0;
    public float attackSpeed = 0;
    public float movementSpeed = 0;
    public int itemAbility = 0;
    public int itemPassive = 0;

    public Sprite itemSprite;
}
