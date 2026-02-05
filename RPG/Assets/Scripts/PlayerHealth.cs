using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
    public int currentHealth = 3;
    public int maxHealth = 3;
    public TMP_Text healthText;
    public Animator healthTextAnimator;

    private void Start()
    {
        healthText.text = "HP:" + currentHealth + "/" + maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        healthTextAnimator.Play("TextUpdate");
        healthText.text = "HP:" + currentHealth + "/" + maxHealth;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
