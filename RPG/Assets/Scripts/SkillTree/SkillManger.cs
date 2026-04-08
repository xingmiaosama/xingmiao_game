using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManger : MonoBehaviour
{
    public PlayerCombat combat;
    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HealthAbilityPointSpent;
    }

    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HealthAbilityPointSpent;
    }

    private void HealthAbilityPointSpent(SkillSlot slot)
    {
        string skillName = slot.skillSO.skillName;
        switch (skillName)
        {
            case "Max Health Boost":
                PlayerStatsManager.Instance.UpdateMaxHealth(1);
                break;
            case "Sword Slash":
                combat.enabled = true;
                break;
        }
    }
}
