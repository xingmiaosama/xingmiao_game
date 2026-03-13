using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockBack : MonoBehaviour
{
    public Rigidbody2D rb;
    public EnemyMovement enemyMovement;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void KnockBack(Transform playerTransform,float knockBackForce,float knockBackTime,float stunTime)
    {
        enemyMovement.ChangeState(EnemyState.KnockBack);
        StartCoroutine(StunTimer(knockBackTime,stunTime));
        Vector2 direction = (transform.position - playerTransform.position).normalized;
        rb.velocity = direction * knockBackForce;
    }

    private IEnumerator StunTimer(float knockBackTime,float stunTime)
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        enemyMovement.ChangeState(EnemyState.Idle);
    }
}
