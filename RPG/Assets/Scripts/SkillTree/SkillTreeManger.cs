using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillTreeManger : MonoBehaviour
{
    public SkillSlot[] skillSlots;
    public TMP_Text skillpointsText;
    public int avaliableSkillPoints;

    private void Start()
    {
        foreach (SkillSlot slot in skillSlots)
        {
            slot.skillButton.onClick.AddListener(slot.TryUpgradeSkill);
        }
        UpdateAbilityPoints(0);
    }

    public void UpdateAbilityPoints(int amount)
    {
        avaliableSkillPoints += amount;
        skillpointsText.text = "points: " + avaliableSkillPoints.ToString();
    }
}
