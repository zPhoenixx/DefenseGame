using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float baseExp = 100;
    public float exponent = 1.5f;

    public float level = 1;
    public float exp = 0;
    public float expToLevel = 100;
    public float maxLevel = 100;

    private float baseAttackDamage = 100;
    private float AttackDamagePerLevel = 10;

    private float baseMagicDamage = 100;
    private float MagicDamagePerLevel = 10;

    private float baseMaxHealth = 1000;
    private float MaxHealthPerLevel = 100;

    private float baseMaxMana = 100;
    private float MaxManaPerLevel = 10;

    private float baseManaRegen = 1;
    private float ManaRegenPerLevel = 0.1f;

    private float baseHealthRegen = 5;
    private float HealthRegenPerLevel = 2.1f;

    private float baseArmor = 30;
    private float ArmorPerLevel = 5;

    private float baseAttackRange = 10;
    private float AttackRangePerLevel = 0.1f;

    private float baseAttackSpeed = 0.5f;
    private float AttackSpeedPerLevel = 0.05f;

    private float baseMovementSpeed = 5f;
    private float MovementSpeedPerLevel = 0.1f;

    public float attackDamage;
    public float magicDamage;
    public float maxHealth;
    public float health;
    public float maxMana;
    public float mana;
    public float manaRegen;
    public float healthRegen;
    public float armor;
    public float attackRange;
    public float attackSpeed;
    public float movementSpeed;

    private float timeToPass;

    private PlayerUIManager UIManager;
    public Billboard healthBar;

    private PlayerInventory inventory;
    private PlayerController controller;
    private PlayerMotor motor;
    private void Awake()
    {
        inventory = GetComponent<PlayerInventory>();
        UIManager = FindObjectOfType<PlayerUIManager>().GetComponent<PlayerUIManager>();
        controller = GetComponent<PlayerController>();
        motor = GetComponent<PlayerMotor>();
    }
    void Start()
    {
        UpdateStats();
        health = maxHealth;
        mana = maxMana;
    }
    void Update()
    {
        Regenerate();
    }
    private void Regenerate()
    {
        if (timeToPass < Time.time)
        {
            timeToPass = Time.time + 1f;
            ChangeHealth(healthRegen, false);
            ChangeMana(manaRegen);
            LevelUp(50);
        }
    }
    private void LevelUp(float _exp)
    {
        if (level == maxLevel)
            return;
        exp += _exp;
        if (exp >= expToLevel)
        {
            float expLeft = exp - expToLevel;
            exp = 0;
            level += 1;
            expToLevel = Mathf.Floor(baseExp * (level * exponent));
            LevelUp(expLeft);
            UpdateStats();
            health = maxHealth;
            mana = maxMana;
        }
        else
            UIManager.UpdateExpBar(level, exp, expToLevel);
    }
    private void ChangeHealth(float amount,bool showBar)
    {
        health += amount;
        if (health < 0)
            health = 0;
        if (health > maxHealth)
            health = maxHealth;
        UIManager.UpdateHealthBar(health, maxHealth, healthRegen);
        healthBar.UpdateBar(health, maxHealth, showBar);
    }
    private void ChangeMana(float amount)
    {
        mana += amount;
        if (mana < 0)
            mana = 0;
        if (mana > maxMana)
            mana = maxMana;
        UIManager.UpdateManaBar(mana, maxMana, manaRegen);
    }
    public void TakeDamage(float damage)
    {
        float dmg = damage * (100 / (100 + armor));
        ChangeHealth(-dmg, true);
    }
    public void AttackTarget(GameObject target)
    {
        EnemyStats enemyStats = target.GetComponent<EnemyStats>();
        enemyStats.TakeDamage(attackDamage);
    }
    public bool UseManaForAbility(float _mana)
    {
        if (mana >= _mana)
        {
            ChangeMana(-_mana);
            return true;
        }
        else
            return false;
    }
    public void UpdateStats()
    {
        attackDamage = baseAttackDamage + (level * AttackDamagePerLevel) + inventory.itemsAttackDamage;
        magicDamage = baseMagicDamage + (level * MagicDamagePerLevel) + inventory.itemsMagicDamage;
        maxHealth = baseMaxHealth + (level * MaxHealthPerLevel) + inventory.itemsMaxHealth;
        maxMana = baseMaxMana + (level * MaxManaPerLevel) + inventory.itemsMaxMana;
        manaRegen = baseManaRegen + (level * ManaRegenPerLevel) + inventory.itemsManaRegen;
        healthRegen = baseHealthRegen + (level * HealthRegenPerLevel) + inventory.itemsHealthRegen;
        armor = baseArmor + (level * ArmorPerLevel) + inventory.itemsArmor;
        attackRange = baseAttackRange + (level * AttackRangePerLevel) + inventory.itemsAttackRange;
        attackSpeed = baseAttackSpeed + (level * AttackSpeedPerLevel) + inventory.itemsAttackSpeed;
        movementSpeed = baseMovementSpeed + (level * MovementSpeedPerLevel) + inventory.itemsMovementSpeed;

        motor.SetMoveSpeed(movementSpeed);
        controller.SetStats(attackSpeed, attackRange);
        UIManager.UpdateStatsUI();
    }
}
