using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ShopInfo : MonoBehaviour
{
    public CanvasGroup infoPanel;
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;

    [Header("Stat Fields")]
    public TMP_Text[] statTexts;

    private RectTransform infoPanelRect;

    private void Awake()
    {
        infoPanelRect = GetComponent<RectTransform>();
    }

    public void ShowItemInfo(ItemSO itemSO)
    {
        infoPanel.alpha = 1;

        itemNameText.text = itemSO.itemName;
        itemDescriptionText.text = itemSO.itemDescription;

        List<string> stats = new List<string>();

        if(itemSO.currentHealth > 0) stats.Add($"Current Health: {itemSO.currentHealth}");
        if(itemSO.maxHealth > 0) stats.Add($"Max Health: {itemSO.maxHealth}");
        if(itemSO.speed > 0) stats.Add($"Speed: {itemSO.speed}");
        if(itemSO.damage > 0) stats.Add($"Damage: {itemSO.damage}");

        if(stats.Count <= 0)
        {
            return;
        }

        for(int i = 0;i < statTexts.Length; i++)
        {
            if(i < stats.Count)
            {
                statTexts[i].text = stats[i];
                statTexts[i].gameObject.SetActive(true);
            }
            else
            {
                statTexts[i].gameObject.SetActive(false);
            }
        }

    }

    public void HideItemInfo()
    {
        infoPanel.alpha = 0;

        itemNameText.text = "";
        itemDescriptionText.text = "";
    }

    public void FollowMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 offset = new Vector3(10,-10,0);

        infoPanelRect.position = mousePosition + offset;
    }
}
