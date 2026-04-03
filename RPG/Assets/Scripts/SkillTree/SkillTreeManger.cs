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
            slot.skillButton.onClick.AddListener(() => CheckAvaliablePoints(slot));
        }
        UpdateAbilityPoints(0);
    }

    private void CheckAvaliablePoints(SkillSlot slot)
    {
        if(avaliableSkillPoints > 0)
        {
            slot.TryUpgradeSkill();
        }
    }

    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointSpent;
        SkillSlot.OnSkillMaxed += HandleSkillMaxed;
        ExpManager.OnLevelUp += UpdateAbilityPoints;
    }


    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointSpent;
        SkillSlot.OnSkillMaxed -= HandleSkillMaxed;
        ExpManager.OnLevelUp -= UpdateAbilityPoints;
    }

    public void UpdateAbilityPoints(int amount)
    {
        avaliableSkillPoints += amount;
        skillpointsText.text = "points: " + avaliableSkillPoints.ToString();
    }

    private void HandleAbilityPointSpent(SkillSlot slot)
    {
        if (avaliableSkillPoints > 0)
        {
            UpdateAbilityPoints(-1);
        }
    }

    private void HandleSkillMaxed(SkillSlot skillSlot)
    {
        foreach (SkillSlot slot in skillSlots)
        {
            if(! slot.isUnLocked && slot.CanUnlockSkill())
            {
                slot.Unlock();
            }
        }
    }
}
