using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWander : MonoBehaviour
{
    [Header("Wander Area")]
    public float wanderWidth;
    public float wanderHeight;
    public Vector2 startingPosition;
    public float pauseDuration;
    public float speed;
    public Vector2 target;
    private Rigidbody2D rb;
    public Animator anim;
    private bool isPaused;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnEnable()
    {
        StartCoroutine(PauseAndPickNewDestination());
    }

    private void Update()
    {
        if (isPaused)
        {
            rb.velocity = Vector2.zero;
            return; 
        }

        if(Vector2.Distance(transform.position,target) < 0.1f)
        {
            StartCoroutine(PauseAndPickNewDestination());
        }

        Move(); 
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(startingPosition,new Vector3(wanderWidth,wanderHeight,0));
    }


    private Vector2 GetRandomTarget()
    {
        float halfWidth = wanderWidth / 2f;
        float halfHeight = wanderHeight / 2f;
        int edge = Random.Range(0,4);

        return edge switch
        {
            0 => new Vector2(startingPosition.x - halfWidth,Random.Range(startingPosition.y - halfHeight,startingPosition.y + halfHeight)),
            1 => new Vector2(startingPosition.x + halfWidth,Random.Range(startingPosition.y - halfHeight,startingPosition.y + halfHeight)),
            2 => new Vector2(Random.Range(startingPosition.x - halfWidth,startingPosition.x + halfWidth),startingPosition.y - halfHeight),
            _ => new Vector2(Random.Range(startingPosition.x - halfWidth,startingPosition.x + halfWidth),startingPosition.y + halfHeight),  
        };
    }

    IEnumerator PauseAndPickNewDestination()
    {
        isPaused = true;
        anim.Play("Idle");
        yield return new WaitForSeconds(pauseDuration);

        target = GetRandomTarget();
        isPaused = false;
        anim.Play("Walk");
    }

    private void Move()
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        if(direction.x < 0 && transform.localScale.x > 0 || direction.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
        }
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(PauseAndPickNewDestination());
    }
}
