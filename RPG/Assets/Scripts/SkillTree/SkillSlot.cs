using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSlot : MonoBehaviour
{
    public SkillSO skillSO;
    public int currentLevel;
    public bool isUnLocked;
    public TMP_Text skillLevelText;
    public UnityEngine.UI.Image skillIcon;
    public Button skillButton;

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
            currentLevel++;
            UpdateUI();
        }
    }
}
