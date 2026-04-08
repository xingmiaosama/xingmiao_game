using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public Animator anim;
    private float timer;

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
            timer = PlayerStatsManager.Instance.coolDownTime;
            anim.SetBool("isAttacking",true);
        }
        
    }

    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position,PlayerStatsManager.Instance.weaponRange,enemyLayer);
        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<EnemyHealth>().ChangeHealth(-PlayerStatsManager.Instance.damage);
            enemies[0].GetComponent<EnemyKnockBack>().KnockBack(transform,PlayerStatsManager.Instance.knockBackForce,PlayerStatsManager.Instance.knockBackTime,PlayerStatsManager.Instance.stunTime);
        }
    }

    public void FinshAttack()
    {
        anim.SetBool("isAttacking",false);
    }

    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(attackPoint.position,PlayerStatsManager.Instance.weaponRange);
    // }

}
