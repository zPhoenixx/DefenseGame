using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour
{
    public Slider healthSlider;
    public Image healthFill;
    public Gradient healthGradient;

    private Canvas canvas;

    private float showDuration = 5f;
    private float showStarted = 0f;
    private bool barShown = false;
    private bool targeted = false;
    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }
    private void Update()
    {
        if (barShown)
        {
            if (Time.time - showStarted >= showDuration)
                HideBar();
        }
    }
    void LateUpdate()
    {
        if (barShown)
        {
            transform.LookAt(transform.position + Camera.main.transform.forward);
        }
    }
    public void UpdateBar(float health, float maxHealth,bool showBar)
    {
        if(showBar)
            ShowBar();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;

        healthFill.color = healthGradient.Evaluate(1f);
        healthFill.color = healthGradient.Evaluate(healthSlider.normalizedValue);
    }
    private void ShowBar()
    {
        canvas.enabled = true;
        barShown = true;
        showStarted = Time.time;
    }
    private void HideBar()
    {
        canvas.enabled = false;
        barShown = false;
    }
}
