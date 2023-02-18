using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    Camera _main;

    [SerializeField] private Transform hpUI;
    private Image hpBar;

    [SerializeField] private int fullHP = 5000;
    [SerializeField] private int currentHP;

    private int previousHP;
    private Vector3 previousPos;

    private SpriteDisplayer spriteDisplayer;

    private void Awake()
    {
        _main = Camera.main;
        hpBar = hpUI.Find("Bar").GetComponent<Image>();
        spriteDisplayer = GetComponent<SpriteDisplayer>();
    }

    private void Update()
    {
        if (previousHP != currentHP || previousPos != transform.position)
        {
            HPUpdate();
        }
    }

    //Esse método atualiza o HP e a estética dos sprites relacionados
    private void HPUpdate()
    {
        Vector3 barPos = transform.position + 1.5f * Vector3.up;
        float healthPercent = (float)currentHP / (float)fullHP;
        SpriteDisplayer.ShipHealth healthState;
        bool isHPDisplayed = true;
        
        hpUI.position = _main.WorldToScreenPoint(barPos);
        hpBar.fillAmount = healthPercent;

        if (healthPercent >= 0.66f)
        {
            hpBar.color = Color.green;
            healthState = SpriteDisplayer.ShipHealth.FULL_HEALTH;
        }
        else if (healthPercent >= 0.33f)
        {
            hpBar.color = Color.yellow;
            healthState = SpriteDisplayer.ShipHealth.LIGHT_DAMAGE;
        }
        else if (healthPercent > 0f)
        {
            hpBar.color = Color.red;
            healthState = SpriteDisplayer.ShipHealth.HEAVY_DAMAGE;
        }
        else
        {
            hpBar.color = Color.red;
            healthState = SpriteDisplayer.ShipHealth.SUNK;
            isHPDisplayed = false;
        }

        previousHP = currentHP;
        previousPos = transform.position;
        spriteDisplayer.SetSprite(healthState);
        hpUI.gameObject.SetActive(isHPDisplayed);
    }

    public void RestoreHP()
    {
        currentHP = fullHP;
        HPUpdate();
    }
}