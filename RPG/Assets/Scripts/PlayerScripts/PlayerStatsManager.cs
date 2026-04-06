using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance;
    public TMP_Text healthText;

    [Header("Movement")]
    public float speed;
    public  int  facingDirection;

    [Header("Health")]
    public int currentHealth;
    public int maxHealth;

    [Header("Combat")]
    public float weaponRange;
    public float coolDownTime;
    public int damage;
    public float knockBackForce;
    public float knockBackTime;
    public float stunTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateMaxHealth(int amount)
    {
        maxHealth += amount;
        healthText.text = "HP: " + currentHealth + "/" + maxHealth;
    }
}
