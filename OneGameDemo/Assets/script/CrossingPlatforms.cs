using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingPlatforms : MonoBehaviour
{
    private PlatformEffector2D effector;
    private ObstacleData obstacleData;
    private bool playerOnPlatform;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        obstacleData = GetComponent<ObstacleData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOnPlatform && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            effector.rotationalOffset = 180f;
            StartCoroutine(ResetEffector());
        }
    }

    private System.Collections.IEnumerator ResetEffector()
    {
        yield return new WaitForSeconds(obstacleData.waitTime);
        effector.rotationalOffset = 0f;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("111");
            playerOnPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("000");
            playerOnPlatform = false;
        }
    }
}
