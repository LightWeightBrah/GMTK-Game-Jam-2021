using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleHud : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private Image healthImage;

    private void OnEnable()
    {
        unit.onUnitTakeDamage += UpdateHealth;
    }

    private void OnDisable()
    {
        unit.onUnitTakeDamage -= UpdateHealth;
    }

    private void UpdateHealth(float currHealth, float maxHealth)
    {
        healthImage.fillAmount = currHealth / maxHealth;
    }
}
