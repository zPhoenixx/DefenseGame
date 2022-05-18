using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI magicText;
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI aspdText;
    public TextMeshProUGUI mspdText;
    public PlayerStats playerStats;

    public Slider expSlider;
    public Image expFill;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;
    public Gradient expGradient;

    public Slider healthSlider;
    public Image healthFill;
    public Slider hudHealthSlider;
    public Image hudHealthFill;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI healthRegenText;
    public Gradient healthGradient;

    public Slider manaSlider;
    public Image manaFill;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI manaRegenText;
    public Gradient manaGradient;

    public ItemSlot[] inventorySlots = new ItemSlot[6];

    public void UpdateHealthBar(float health,float maxHealth,float healthRegen)
    {
        healthSlider.maxValue = Mathf.Ceil(maxHealth);
        healthSlider.value = Mathf.Ceil(health);
        healthText.text = "" + Mathf.Ceil(health) + "/" + Mathf.Ceil(maxHealth);
        healthRegenText.text = "" + Mathf.Ceil(healthRegen) + "/s";
        healthFill.color = healthGradient.Evaluate(1f);
        healthFill.color = healthGradient.Evaluate(healthSlider.normalizedValue);

        hudHealthSlider.maxValue = Mathf.Ceil(maxHealth);
        hudHealthSlider.value = Mathf.Ceil(health);
        hudHealthFill.color = healthGradient.Evaluate(1f);
        hudHealthFill.color = healthGradient.Evaluate(healthSlider.normalizedValue);
    }
    public void UpdateManaBar(float mana, float maxMana, float manaRegen)
    {
        manaSlider.maxValue = maxMana;
        manaSlider.value = mana;
        manaText.text = "" + mana + "/" + maxMana;
        manaRegenText.text = "" + manaRegen + "/s";
        manaFill.color = manaGradient.Evaluate(1f);
        manaFill.color = manaGradient.Evaluate(manaSlider.normalizedValue);
    }
    public void UpdateExpBar(float level,float exp,float expToNextLevel)
    {
        expSlider.maxValue = Mathf.Ceil(expToNextLevel);
        expSlider.value = Mathf.Ceil(exp);
        levelText.text = level.ToString();
        expText.text = "" + Mathf.Ceil(exp) + "/" + Mathf.Ceil(expToNextLevel);
        expFill.color = expGradient.Evaluate(1f);
        expFill.color = expGradient.Evaluate(expSlider.normalizedValue);
    }
    public void UpdateInventory(Item[] inventory)
    {
        for (int i = 0; i < 6; i++)
        {
            if (inventory[i] != null)
                inventorySlots[i].AddItem(inventory[i]);
            else
                inventorySlots[i].RemoveItem();
        }
    }
    public void UpdateStatsUI()
    {
        attackText.text = playerStats.attackDamage.ToString("F0");
        magicText.text = playerStats.magicDamage.ToString("F0");
        armorText.text = playerStats.armor.ToString("F0");
        rangeText.text = playerStats.attackRange.ToString("F1");
        aspdText.text = playerStats.attackSpeed.ToString("F1");
        mspdText.text = playerStats.movementSpeed.ToString("F1");
    }
}
