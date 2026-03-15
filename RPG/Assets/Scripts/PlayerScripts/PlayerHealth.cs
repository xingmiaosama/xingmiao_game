using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
    public TMP_Text healthText;
    public Animator healthTextAnimator;

    private void Start()
    {
        healthText.text = "HP:" + PlayerStatsManager.Instance.currentHealth + "/" + PlayerStatsManager.Instance.maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        PlayerStatsManager.Instance.currentHealth += amount;
        healthTextAnimator.Play("TextUpdate");
        healthText.text = "HP:" + PlayerStatsManager.Instance.currentHealth + "/" + PlayerStatsManager.Instance.maxHealth;

        if (PlayerStatsManager.Instance.currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
