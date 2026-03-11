using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float weaponRange = 0.8f;
    public LayerMask enemyLayer;
    public Animator anim;
    public float coolDownTime = 1f;
    private float timer;
    public int damage = 1;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (timer <= 0)
        {
            timer = coolDownTime;
            anim.SetBool("isAttacking",true);
        }
        
    }

    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position,weaponRange,enemyLayer);
        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<EnemyHealth>().ChangeHealth(-damage);
        }
    }

    public void FinshAttack()
    {
        anim.SetBool("isAttacking",false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position,weaponRange);
    }

}
