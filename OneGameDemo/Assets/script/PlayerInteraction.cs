using UnityEngine;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
    [Header("交互设置")]
    public float interactionRange = 2f;
    public KeyCode interactionKey = KeyCode.F;
    public LayerMask interactableLayer = 1; // 默认层

    private ChestInventory nearbyChest;
    private bool canInteract = false;

    void Update()
    {
        CheckForChest();
        
        if (canInteract && Input.GetKeyDown(interactionKey))
        {
            if (nearbyChest != null)
            {
                nearbyChest.ToggleChest();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAllChests();
        }
    }
    
    void CheckForChest()
    {
        // 添加null检查
        if (this == null) return;
        
        // 使用LayerMask来只检测特定层的物体
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, interactionRange, interactableLayer);
        
        ChestInventory closestChest = null;
        float closestDistance = Mathf.Infinity;
        
        foreach (var hitCollider in hitColliders)
        {
            // 添加null检查
            if (hitCollider == null) continue;
            
            ChestInventory chest = hitCollider.GetComponent<ChestInventory>();
            if (chest != null)
            {
                float distance = Vector2.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestChest = chest;
                }
            }
        }
        
        // 更新交互状态 - 添加更多安全检查
        if (nearbyChest != closestChest)
        {
            // 安全地移除旧方块的提示
            if (nearbyChest != null)
            {
                // 使用安全方法而不是直接访问
                SafeSetInteractHint(nearbyChest, false);
            }
            
            nearbyChest = closestChest;
            canInteract = (nearbyChest != null);
            
            // 安全地显示新方块的提示
            if (nearbyChest != null)
            {
                SafeSetInteractHint(nearbyChest, true);
            }
        }
    }
    
    // 安全设置交互提示的方法
    private void SafeSetInteractHint(ChestInventory chest, bool show)
    {
        if (chest == null) return;
        
        // 使用try-catch来避免异常
        try
        {
            if (chest.interactHint != null)
            {
                chest.interactHint.SetActive(show);
            }
            else
            {
                Debug.LogWarning($"Chest {chest.name} 的 interactHint 未设置");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"设置交互提示时出错: {e.Message}");
        }
    }
    
    void CloseAllChests()
    {
        ChestInventory[] allChests = FindObjectsOfType<ChestInventory>();
        foreach (ChestInventory chest in allChests)
        {
            if (chest != null && chest.isOpen)
            {
                chest.CloseChest();
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}