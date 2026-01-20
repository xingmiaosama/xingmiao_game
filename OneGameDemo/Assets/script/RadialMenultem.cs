using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 轮盘菜单项类
public class RadialMenuItem : MonoBehaviour
{
    // 菜单项状态枚举
    public enum State { Normal, Highlighted, Selected }
    
    [Header("UI References")]
    public Image background;                     // 背景图像组件
    public Image iconImage;                      // 图标图像组件
    public TextMeshProUGUI label;                // 标签文本组件
    public Button button;                        // 按钮组件
    
    [Header("Colors")]
    public Color normalColor = Color.white;      // 正常状态颜色
    public Color highlightedColor = Color.yellow;// 高亮状态颜色
    public Color selectedColor = Color.green;    // 选中状态颜色
    
    private RadialMenuController.MenuItemData itemData; // 菜单项数据引用
    
    // 初始化菜单项函数
    public void Initialize(RadialMenuController.MenuItemData data)
    {
        itemData = data;                         // 保存菜单项数据
        
        if (iconImage != null && data.icon != null) // 如果有图标组件和数据图标
        {
            iconImage.sprite = data.icon;        // 设置图标
        }
        
        if (label != null)                       // 如果有标签组件
        {
            label.text = data.name;              // 设置标签文本
        }
        
        // 确保有Button组件
        if (button == null)                      // 如果按钮引用为空
        {
            button = GetComponent<Button>();     // 获取按钮组件
        }
    }
    
    // 设置菜单项状态函数
    public void SetState(State state)
    {
        Color targetColor = normalColor;         // 默认目标颜色为正常颜色
        
        switch (state)                           // 根据状态选择颜色
        {
            case State.Normal:
                targetColor = normalColor;       // 正常状态使用正常颜色
                break;
            case State.Highlighted:
                targetColor = highlightedColor;  // 高亮状态使用高亮颜色
                break;
            case State.Selected:
                targetColor = selectedColor;     // 选中状态使用选中颜色
                break;
        }
        
        if (background != null)                  // 如果有背景组件
        {
            background.color = targetColor;      // 设置背景颜色
        }
        
        // 状态变化动画
        if (state == State.Highlighted)          // 如果是高亮状态
        {
            transform.localScale = Vector3.one * 1.2f; // 放大1.2倍
        }
        else                                     // 其他状态
        {
            transform.localScale = Vector3.one;  // 恢复原始大小
        }
        
        // 更新按钮交互状态
        if (button != null)                      // 如果有按钮组件
        {
            ColorBlock colors = button.colors;   // 获取按钮颜色块
            colors.normalColor = targetColor;    // 设置正常状态颜色
            colors.highlightedColor = highlightedColor; // 设置高亮状态颜色
            colors.pressedColor = selectedColor; // 设置按下状态颜色
            button.colors = colors;              // 应用颜色设置
        }
    }
}