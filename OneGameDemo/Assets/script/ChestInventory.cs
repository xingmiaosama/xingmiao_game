using UnityEngine;
using System.Collections;

public class ChestInventory : MonoBehaviour
{
    [Header("物品栏设置")]
    public GameObject inventoryUI;
    public bool isOpen = false;
    public PlayerController playerController;
    
    [Header("交互提示")]
    public GameObject interactHint;
    
    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        // 添加调试信息
        if (inventoryUI == null)
            Debug.LogWarning($"{gameObject.name} 的 inventoryUI 未设置");
            
        if (interactHint == null)
            Debug.LogWarning($"{gameObject.name} 的 interactHint 未设置");
        
        // 安全的初始化
        SafeSetActive(inventoryUI, false);
        SafeSetActive(interactHint, false);
    }
    
    public void OpenChest()
    {
        isOpen = true;
        SafeSetActive(inventoryUI, true);
        playerController.enabled = false;
        Debug.Log($"打开 {gameObject.name} 的物品栏");
    }
    
    public void CloseChest()
    {
        isOpen = false;
        SafeSetActive(inventoryUI, false);
        playerController.enabled = true;
        Debug.Log($"关闭 {gameObject.name} 的物品栏");
    }
    
    public void ToggleChest()
    {
        if (isOpen)
            CloseChest();
        else
            OpenChest();
    }
    
    // 安全设置GameObject激活状态
    private void SafeSetActive(GameObject obj, bool active)
    {
        if (obj != null)
        {
            obj.SetActive(active);
        }
    }
}