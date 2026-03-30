using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SkillSlot : MonoBehaviour
{
    public SkillSO skillSO;
    public int currentLevel;
    public bool isUnLocked;
    public TMP_Text skillLevelText;
    public UnityEngine.UI.Image skillIcon;
    public Button skillButton;
    public List<SkillSlot> prerequisiteSlots;

    public static event Action<SkillSlot> OnAbilityPointSpent;
    public static event Action<SkillSlot> OnSkillMaxed;

    private void OnValidate()
    {
        if (skillSO != null && skillLevelText != null)
        {
            UpdateUI();
        }
    } 

    void UpdateUI()
    {
        skillIcon.sprite = skillSO.skillIcon;

        if (isUnLocked)
        {
            skillButton.interactable = true;
            skillLevelText.text = currentLevel.ToString() + "/" + skillSO.maxLevel.ToString();
            skillIcon.color = Color.white;
        }
        else
        {
            skillButton.interactable = false;
            skillLevelText.text = "Locked";
            skillIcon.color = Color.gray;
        }
    }

    public void TryUpgradeSkill()
    {
        if (isUnLocked && currentLevel < skillSO.maxLevel)
        {
            OnAbilityPointSpent?.Invoke(this);
            currentLevel++;
            if (currentLevel >= skillSO.maxLevel)
            {
                OnSkillMaxed?.Invoke(this);
            }
            UpdateUI();
        }
    }

    public bool CanUnlockSkill()
    {
        foreach (SkillSlot slot in prerequisiteSlots)
        {
            if (! slot.isUnLocked || slot.currentLevel < slot.skillSO.maxLevel)
            {
                return false;
            } 
        }
        return true;
    }

    public void Unlock()
    {
        isUnLocked = true;
        UpdateUI();
    }

}
