using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{

    public ItemSO itemSO;
    public TMP_Text itemNameText;
    public TMP_Text priceText;
    public Image itemImage;

    public int price;

    public void Initialize(ItemSO itemSO,int price)
    {
        this.itemSO = itemSO;
        this.price = price;
        itemImage.sprite = itemSO.icon;
        itemNameText.text = itemSO.itemName;
        priceText.text = price.ToString();
    }
}
