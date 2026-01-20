using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// 轮盘菜单控制器类，实现指针事件接口用于处理拖拽交互
public class RadialMenuController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Header("UI Elements")]
    public RectTransform radialMenuPanel;        // 轮盘菜单面板的矩形变换组件
    public GameObject menuItemPrefab;            // 菜单项预制体
    public Button centerCancelButton;            // 中心取消按钮
    public TextMeshProUGUI selectedItemText;     // 显示选中项名称的文本组件
    
    [Header("Menu Settings")]
    public int itemCount = 6;                    // 菜单项数量
    public float radius = 150f;                  // 菜单项排列半径
    public float deadZone = 0.2f;                // 输入死区，防止轻微移动误触
    
    [Header("Visual Feedback")]
    public Color normalColor = Color.white;      // 正常状态颜色
    public Color highlightedColor = Color.yellow;// 高亮状态颜色
    public Color selectedColor = Color.green;    // 选中状态颜色
    public float animationDuration = 0.2f;       // 动画持续时间
    
    private List<RadialMenuItem> menuItems = new List<RadialMenuItem>(); // 菜单项列表
    private bool isMenuActive = false;           // 菜单是否激活标志
    private int currentSelection = -1;           // 当前选中项索引
    private int previousSelection = -1;          // 之前选中项索引
    private Vector2 inputDirection = Vector2.zero; // 输入方向向量
    
    // 菜单项数据类，存储菜单项信息
    [System.Serializable]
    public class MenuItemData
    {
        public string name;                      // 菜单项名称
        public Sprite icon;                      // 菜单项图标
        public System.Action onSelect;           // 选中时的回调函数
    }
    
    private List<MenuItemData> menuData = new List<MenuItemData>(); // 菜单数据列表
    
    // 事件定义
    public System.Action<int> OnItemSelected;    // 菜单项选中事件
    public System.Action OnMenuCanceled;         // 菜单取消事件
    
    // 初始化函数，在游戏开始时调用
    void Start()
    {
        InitializeMenu();                        // 初始化菜单
        
        // 设置取消按钮事件
        if (centerCancelButton != null)
        {
            // 添加取消按钮点击监听
            centerCancelButton.onClick.AddListener(OnCancelButtonClicked);
        }
        
        HideMenu();                              // 初始隐藏菜单
    }
    
    // 每帧更新函数
    void Update()
    {
        // 键盘/控制器输入检测
        if (isMenuActive)
        {
            HandleControllerInput();             // 处理控制器输入
        }
        
        // 测试用 - 在实际项目中通过其他方式触发
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMenu();                        // 切换菜单显示状态
        }
        
        // ESC键取消菜单
        if (isMenuActive && Input.GetKeyDown(KeyCode.Escape))
        {
            CancelMenu();                        // 取消菜单
        }
    }
    
    // 初始化菜单函数
    void InitializeMenu()
    {
        // 创建示例菜单数据
        for (int i = 0; i < itemCount; i++)
        {
            int index = i; // 闭包捕获需要局部变量，避免引用问题
            MenuItemData data = new MenuItemData
            {
                name = $"Item {i + 1}",          // 设置菜单项名称
                icon = null,                     // 图标初始为空
                onSelect = () => { 
                    //Debug.Log($"Selected: {data.name}"); // 选中时打印日志
                    OnItemSelected?.Invoke(index); // 触发选中事件
                }
            };
            menuData.Add(data);                  // 添加菜单数据到列表
        }
        
        // 创建UI元素
        for (int i = 0; i < itemCount; i++)
        {
            GameObject itemObj = Instantiate(menuItemPrefab, radialMenuPanel); // 实例化菜单项
            RadialMenuItem item = itemObj.GetComponent<RadialMenuItem>(); // 获取菜单项组件
            
            if (item != null)
            {
                item.Initialize(menuData[i]);    // 初始化菜单项
                
                // 为按钮添加点击事件
                Button itemButton = item.GetComponent<Button>();
                if (itemButton != null)
                {
                    int itemIndex = i; // 闭包捕获
                    itemButton.onClick.AddListener(() => OnMenuItemClicked(itemIndex)); // 添加点击监听
                }
                
                menuItems.Add(item);             // 添加到菜单项列表
            }
        }
        
        // 排列菜单项
        ArrangeMenuItems();
    }
    
    // 排列菜单项函数
    void ArrangeMenuItems()
    {
        float angleStep = 360f / itemCount;      // 计算每个菜单项的角度间隔
        
        for (int i = 0; i < menuItems.Count; i++)
        {
            float angle = i * angleStep;         // 计算当前菜单项的角度
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)); // 计算方向向量
            Vector2 position = direction * radius; // 计算位置
            
            RectTransform itemRect = menuItems[i].GetComponent<RectTransform>(); // 获取矩形变换组件
            itemRect.anchoredPosition = position; // 设置位置
            
            // 调整图标旋转使其朝向中心
            Image iconImage = menuItems[i].iconImage;
            if (iconImage != null)
            {
                iconImage.transform.rotation = Quaternion.Euler(0, 0, -angle); // 旋转图标
            }
        }
    }
    
    // 处理控制器输入函数
    void HandleControllerInput()
    {
        // 获取输入方向
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        // 应用死区，忽略轻微输入
        if (input.magnitude < deadZone)
        {
            inputDirection = Vector2.zero;       // 输入方向归零
            ClearSelection();                    // 清除选择
            return;
        }
        
        inputDirection = input.normalized;       // 归一化输入方向
        UpdateSelection();                       // 更新选择
    }
    
    // 指针按下事件处理
    public void OnPointerDown(PointerEventData eventData)
    {
        // 检查是否点击在取消按钮上
        if (centerCancelButton != null && RectTransformUtility.RectangleContainsScreenPoint(
            centerCancelButton.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera))
        {
            return; // 让取消按钮处理点击，不激活菜单
        }
        
        isMenuActive = true;                     // 激活菜单
        inputDirection = Vector2.zero;           // 重置输入方向
    }
    
    // 拖拽事件处理
    public void OnDrag(PointerEventData eventData)
    {
        if (!isMenuActive) return;               // 如果菜单未激活，直接返回
        
        // 将屏幕坐标转换为UI局部坐标
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            radialMenuPanel, eventData.position, eventData.pressEventCamera, out localPoint);
        
        inputDirection = localPoint.normalized;  // 计算输入方向
        UpdateSelection();                       // 更新选择
    }
    
    // 指针抬起事件处理
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isMenuActive) return;               // 如果菜单未激活，直接返回
        
        // 检查是否在取消按钮上释放
        if (centerCancelButton != null && RectTransformUtility.RectangleContainsScreenPoint(
            centerCancelButton.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera))
        {
            CancelMenu();                        // 取消菜单
            return;
        }
        
        if (currentSelection >= 0)               // 如果有选中项
        {
            SelectItem(currentSelection);        // 选择该项
        }
        
        isMenuActive = false;                    // 取消激活菜单
        ClearSelection();                        // 清除选择
        HideMenu();                              // 隐藏菜单
    }
    
    // 菜单项点击处理函数
    void OnMenuItemClicked(int index)
    {
        // 直接点击菜单项
        SelectItem(index);                       // 选择该项
        HideMenu();                              // 隐藏菜单
    }
    
    // 取消按钮点击处理函数
    void OnCancelButtonClicked()
    {
        CancelMenu();                            // 取消菜单
    }
    
    // 更新选择函数
    void UpdateSelection()
    {
        // 计算当前角度对应的选项
        float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg; // 计算角度
        if (angle < 0) angle += 360;             // 确保角度在0-360范围内
        
        float angleStep = 360f / itemCount;      // 计算角度间隔
        currentSelection = Mathf.FloorToInt(angle / angleStep); // 计算当前选中项索引
        
        // 更新UI反馈
        if (currentSelection != previousSelection) // 如果选择发生变化
        {
            if (previousSelection >= 0 && previousSelection < menuItems.Count) // 如果之前有选中项
            {
                menuItems[previousSelection].SetState(RadialMenuItem.State.Normal); // 恢复之前项为正常状态
            }
            
            if (currentSelection >= 0 && currentSelection < menuItems.Count) // 如果当前有选中项
            {
                menuItems[currentSelection].SetState(RadialMenuItem.State.Highlighted); // 设置当前项为高亮状态
                selectedItemText.text = menuData[currentSelection].name; // 更新选中项文本
            }
            
            previousSelection = currentSelection; // 更新之前选中项
        }
    }
    
    // 清除选择函数
    void ClearSelection()
    {
        if (previousSelection >= 0 && previousSelection < menuItems.Count) // 如果之前有选中项
        {
            menuItems[previousSelection].SetState(RadialMenuItem.State.Normal); // 恢复为正常状态
        }
        
        currentSelection = -1;                   // 重置当前选中项
        previousSelection = -1;                  // 重置之前选中项
        selectedItemText.text = "";              // 清空选中项文本
    }
    
    // 选择菜单项函数
    void SelectItem(int index)
    {
        if (index >= 0 && index < menuItems.Count) // 确保索引有效
        {
            menuItems[index].SetState(RadialMenuItem.State.Selected); // 设置选中状态
            menuData[index].onSelect?.Invoke();  // 调用选中回调
            
            // 触发选择事件
            OnItemSelected?.Invoke(index);       // 触发选中事件
            
            // 选择反馈动画
            StartCoroutine(SelectionFeedback()); // 启动选择反馈协程
        }
    }
    
    // 取消菜单函数
    void CancelMenu()
    {
        Debug.Log("Menu canceled");              // 打印取消日志
        OnMenuCanceled?.Invoke();                // 触发取消事件
        ClearSelection();                        // 清除选择
        HideMenu();                              // 隐藏菜单
    }
    
    // 选择反馈协程
    IEnumerator SelectionFeedback()
    {
        // 选择反馈动画
        radialMenuPanel.localScale = Vector3.one * 1.1f; // 放大面板
        yield return new WaitForSeconds(0.1f);   // 等待0.1秒
        radialMenuPanel.localScale = Vector3.one; // 恢复面板大小
    }
    
    // 显示菜单函数
    public void ShowMenu()
    {
        radialMenuPanel.gameObject.SetActive(true); // 激活菜单面板
        isMenuActive = true;                     // 设置菜单激活标志
        
        // 显示动画
        radialMenuPanel.localScale = Vector3.zero; // 初始缩放为0
        StartCoroutine(AnimateMenu(Vector3.zero, Vector3.one, animationDuration)); // 启动动画协程
    }
    
    // 隐藏菜单函数
    public void HideMenu()
    {
        isMenuActive = false;                    // 设置菜单未激活标志
        
        // 隐藏动画
        StartCoroutine(AnimateMenu(Vector3.one, Vector3.zero, animationDuration, 
            () => radialMenuPanel.gameObject.SetActive(false))); // 启动动画协程，完成后禁用面板
    }
    
    // 切换菜单显示状态函数
    public void ToggleMenu()
    {
        if (isMenuActive)
        {
            HideMenu();                          // 如果激活则隐藏
        }
        else
        {
            ShowMenu();                          // 如果未激活则显示
        }
    }
    
    // 菜单动画协程
    IEnumerator AnimateMenu(Vector3 from, Vector3 to, float duration, System.Action onComplete = null)
    {
        float time = 0;                          // 初始化时间
        while (time < duration)                  // 当时间小于持续时间时循环
        {
            time += Time.deltaTime;              // 累加时间
            float t = time / duration;           // 计算插值参数
            radialMenuPanel.localScale = Vector3.Lerp(from, to, EaseOutBack(t)); // 应用缓动插值
            yield return null;                   // 等待下一帧
        }
        
        radialMenuPanel.localScale = to;         // 确保最终状态
        onComplete?.Invoke();                    // 调用完成回调
    }
    
    // 缓动函数 - 提供更流畅的动画
    float EaseOutBack(float t)
    {
        float c1 = 1.70158f;                    // 缓动参数1
        float c3 = c1 + 1;                      // 缓动参数2
        return 1 + c3 * Mathf.Pow(t - 1, 3) + c1 * Mathf.Pow(t - 1, 2); // 计算缓动值
    }
    
    // 公共方法，用于外部设置菜单数据
    public void SetMenuData(List<MenuItemData> newMenuData)
    {
        menuData = newMenuData;                  // 更新菜单数据
        RefreshMenu();                           // 刷新菜单
    }
    
    // 刷新菜单函数
    void RefreshMenu()
    {
        // 清除现有菜单项
        foreach (var item in menuItems)
        {
            Destroy(item.gameObject);            // 销毁菜单项游戏对象
        }
        menuItems.Clear();                       // 清空菜单项列表
        
        // 重新创建菜单项
        for (int i = 0; i < menuData.Count; i++)
        {
            GameObject itemObj = Instantiate(menuItemPrefab, radialMenuPanel); // 实例化新菜单项
            RadialMenuItem item = itemObj.GetComponent<RadialMenuItem>(); // 获取菜单项组件
            
            if (item != null)
            {
                item.Initialize(menuData[i]);    // 初始化菜单项
                
                // 为按钮添加点击事件
                Button itemButton = item.GetComponent<Button>();
                if (itemButton != null)
                {
                    int itemIndex = i;           // 闭包捕获
                    itemButton.onClick.AddListener(() => OnMenuItemClicked(itemIndex)); // 添加点击监听
                }
                
                menuItems.Add(item);             // 添加到菜单项列表
            }
        }
        
        // 重新排列
        ArrangeMenuItems();                      // 重新排列菜单项
    }
}