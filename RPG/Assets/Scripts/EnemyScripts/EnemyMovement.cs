using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    private int facingDirection = -1;
    public float attackRange = 1.2f;
    public float attackCoolDown = 2f;
    public float playerDetectionRange = 5f;

    public LayerMask playerLayer;
    public Transform detectionPoint;
    private float attackCoolDownTimer;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform player;

    private EnemyState enemyState;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        speed = 4f;
        player = null;
        transform.localScale = new Vector3(facingDirection,transform.localScale.y,transform.localScale.z);
        ChangeState(EnemyState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyState != EnemyState.KnockBack)
        {
            CheckForPlayer();
            if (attackCoolDownTimer > 0)
            {
                attackCoolDownTimer -= Time.deltaTime;
            }
            if(enemyState == EnemyState.Chasing)
            {
                Chase();
            }
            else if (enemyState == EnemyState.Attacking)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    void Chase()
    {
        if(player.transform.position.x > transform.position.x && facingDirection == -1 ||
                player.transform.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();
        }
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position,playerDetectionRange,playerLayer);

        if (hits.Length > 0)
        {   
            player = hits[0].transform;

            if(Vector2.Distance(transform.position,player.transform.position) < attackRange && attackCoolDownTimer <= 0)
            {
                attackCoolDownTimer = attackCoolDown;
                ChangeState(EnemyState.Attacking);
            }
            else if (Vector2.Distance(transform.position,player.transform.position) > attackRange && enemyState != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    public void ChangeState(EnemyState newState)
    {
        if(enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle",false);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing",false);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking",false);
        }

        enemyState = newState;

        if(enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle",true);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing",true);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking",true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position,playerDetectionRange);
    }

}

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
    KnockBack
}