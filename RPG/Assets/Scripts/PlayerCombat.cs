using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;

    public void Attack()
    {
        anim.SetBool("isAttacking",true);
    }

    public void FinshAttack()
    {
        anim.SetBool("isAttacking",false);
    }

}
