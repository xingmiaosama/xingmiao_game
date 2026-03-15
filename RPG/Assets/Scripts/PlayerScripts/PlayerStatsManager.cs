using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance;

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
}
