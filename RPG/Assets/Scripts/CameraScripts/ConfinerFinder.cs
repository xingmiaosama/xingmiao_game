using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class ConfinerFinder : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        CinemachineConfiner2D confiner = GetComponent<CinemachineConfiner2D>();
        if (confiner == null)
        {
            Debug.LogError($"ConfinerFinder: 在 {gameObject.name} 上未找到 CinemachineConfiner2D 组件");
            return;
        }

        GameObject confinerObj = GameObject.FindWithTag("Confiner");
        if (confinerObj == null)
        {
            Debug.LogWarning($"ConfinerFinder: 场景 {scene.name} 中没有标记为 'Confiner' 的游戏对象");
            return;
        }

        PolygonCollider2D polygon = confinerObj.GetComponent<PolygonCollider2D>();
        if (polygon == null)
        {
            Debug.LogWarning($"ConfinerFinder: 标记为 'Confiner' 的对象 {confinerObj.name} 没有 PolygonCollider2D 组件");
            return;
        }

        confiner.m_BoundingShape2D = polygon;
    }
}
