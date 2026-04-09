using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.right;
    public float speed;
    public float lifeTime = 2f;
    public LayerMask enemyLayer;

    void Start()
    {
        rb.velocity = direction * speed;
        RotateArrow();
        Destroy(gameObject,lifeTime);
    }

    private void RotateArrow()
    {
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            collision.gameObject.GetComponent<EnemyHealth>().ChangeHealth(-PlayerStatsManager.Instance.damage);
            collision.gameObject.GetComponent<EnemyKnockBack>().KnockBack(transform,PlayerStatsManager.Instance.knockBackForce,PlayerStatsManager.Instance.knockBackTime,PlayerStatsManager.Instance.stunTime);
        }
    }
}
