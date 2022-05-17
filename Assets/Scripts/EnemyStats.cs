using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyStats : MonoBehaviour
{
    public float attackDamage = 100;
    public float magicDamage = 100;
    public float maxHealth = 100;
    public float health = 100;
    public float maxMana = 0;
    public float mana = 0;
    public float manaRegen = 0;
    public float healthRegen = 0;
    public float armor = 0;

    public float attackSpeed = 1f;
    public float attackRange = 2f;
    public float movementSpeed = 3f;

    private EnemyMotor motor;
    private EnemyController controller;
    public Billboard healthBar;
    private void Awake()
    {
        motor = GetComponent<EnemyMotor>();
        controller = GetComponent<EnemyController>();
    }
    void Start()
    {
        motor.SetMoveSpeed(movementSpeed);
        controller.SetStats(attackSpeed, attackRange);
    }
    public void TakeDamage(float damage)
    {
        float dmg = damage * (100 / (100 + armor));
        ChangeHealth(-dmg);
    }
    public void AttackTarget(GameObject target)
    {
        PlayerStats playerStats = target.GetComponent<PlayerStats>();
        playerStats.TakeDamage(attackDamage);
    }
    private void ChangeHealth(float amount)
    {
        health += amount;
        if (health < 0)
            health = 0;
        if (health > maxHealth)
            health = maxHealth;
        healthBar.UpdateBar(health, maxHealth, true);
    }
}
