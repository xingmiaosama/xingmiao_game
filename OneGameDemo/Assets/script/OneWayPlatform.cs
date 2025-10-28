using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private bool playerOnPlatform;
    [SerializeField] private float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        waitTime = 0.50f;
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
        yield return new WaitForSeconds(waitTime);
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
