using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots;
    public CanvasGroup statsCanvas;
    private bool statsOpen = false;
    void Start()
    {
        UpdateAllStats();
        statsOpen = false;
        statsCanvas.alpha = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if(statsOpen)
            {
                Time.timeScale = 1;
                UpdateAllStats();
                statsCanvas.alpha = 0;
                statsOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                UpdateAllStats();
                statsCanvas.alpha = 1;
                statsOpen = true;
            }
    }
    void UpdateDamage()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "Damage: " + PlayerStatsManager.Instance.damage;
    }
    void UpdateSpeed()
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "Speed: " + PlayerStatsManager.Instance.speed;
    }

    public void UpdateAllStats()
    {
        UpdateDamage();
        UpdateSpeed();
    }
}
