using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int damage = 1;
    public Transform attackPoint;
    public float weaponRange = 1.2f;
    public float stunTime;
    public float knockBackForce;
    public LayerMask playerLayer;
    
    void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position,weaponRange,playerLayer);
        if (hits.Length > 0)
        {
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);
            hits[0].GetComponent<PlayerMovement>().KnockedBack(transform,knockBackForce,stunTime);
        }
    }
}
