using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Unit : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform popupPosition;
    [SerializeField] private float maxPopupOffset;
    [SerializeField] private GameObject damagePopup;
    [SerializeField] private int maxHealth;

    public int currentHealth { get; set; }

    public event Action<float, float> onUnitTakeDamage;

    public virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual bool TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);

        onUnitTakeDamage?.Invoke(currentHealth, maxHealth);
        AddDamagePopup(amount);

        if (currentHealth == 0)
        {
            Die();
            return true;
        }

        return false;
    }

    private void AddDamagePopup(int damage)
    {
        var popup = Instantiate(damagePopup, popupPosition.position, Quaternion.identity);

        Vector2 randomPos = (Vector2)popupPosition.transform.position;
        randomPos += new Vector2(UnityEngine.Random.Range(-maxPopupOffset, maxPopupOffset), UnityEngine.Random.Range(-maxPopupOffset, maxPopupOffset));
        popup.transform.position = randomPos;

        popup.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
        Destroy(popup, 3f);
    }

    public virtual void Die()
    {

    }
}
