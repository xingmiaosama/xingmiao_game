using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPatrol : MonoBehaviour
{
    public Vector2[] patrolPoints;
    private Vector2 target;
    private int currentPointIndex;
    public float speed;
    public float pauseDuration;
    private bool isPaused;
    private Rigidbody2D rb;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(SetPatrolPoint());
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector2 direction = (target - (Vector2)transform.position).normalized;
        if(direction.x < 0 && transform.localScale.x > 0 || direction.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
        }
        rb.velocity = direction * speed;

        if(Vector2.Distance(transform.position,target) < 0.1f)
        {
            StartCoroutine(SetPatrolPoint());
        }
    }

    IEnumerator SetPatrolPoint()
    {
        isPaused = true;
        anim.Play("Idle");
        yield return new WaitForSeconds(pauseDuration);

        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        target = patrolPoints[currentPointIndex];
        isPaused = false;
        anim.Play("Walk");
    } 
}
