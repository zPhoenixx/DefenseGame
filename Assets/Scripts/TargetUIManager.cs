using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetUIManager : MonoBehaviour
{
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI magicText;
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI aspdText;
    public TextMeshProUGUI mspdText;

    public Slider healthSlider;
    public Image healthFill;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI healthRegenText;
    public Gradient healthGradient;

    public Slider manaSlider;
    public Image manaFill;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI manaRegenText;
    public Gradient manaGradient;

    private EnemyStats target;
    public GameObject targetPanel;

    void Update()
    {
        if (target != null)
        {
            UpdateUI();
        }
    }
    public void SetTarget(EnemyStats _target)
    {
        targetPanel.SetActive(true);
        target = _target;
    }
    public void RemoveTarget()
    {
        targetPanel.SetActive(false);
        target = null;
    }
    private void UpdateUI()
    {
        attackText.text = target.attackDamage.ToString();
        magicText.text = target.magicDamage.ToString();
        armorText.text = target.armor.ToString();
        rangeText.text = target.attackRange.ToString();
        aspdText.text = target.attackSpeed.ToString();
        mspdText.text = target.movementSpeed.ToString();

        healthSlider.maxValue = Mathf.Ceil(target.maxHealth);
        healthSlider.value = Mathf.Ceil(target.health);
        healthText.text = "" + Mathf.Ceil(target.health) + "/" + Mathf.Ceil(target.maxHealth);
        healthRegenText.text = "" + Mathf.Ceil(target.healthRegen) + "/s";
        healthFill.color = healthGradient.Evaluate(1f);
        healthFill.color = healthGradient.Evaluate(healthSlider.normalizedValue);

        manaSlider.maxValue = target.maxMana;
        manaSlider.value = target.mana;
        manaText.text = "" + target.mana + "/" + target.maxMana;
        manaRegenText.text = "" + target.manaRegen + "/s";
        manaFill.color = manaGradient.Evaluate(1f);
        manaFill.color = manaGradient.Evaluate(manaSlider.normalizedValue);
    }
}
