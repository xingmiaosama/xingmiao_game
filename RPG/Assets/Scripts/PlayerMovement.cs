using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public  int  facingDirection = 1;
    public Rigidbody2D rb;
    public Animator anim;
    public PlayerCombat playerCombat;
    private bool isKnockedBack;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10;
    }

    void Update()
    {
        if (Input.GetButtonDown("Slash"))
        {
            playerCombat.Attack();
        }
    }

    void FixedUpdate()
    {
        if (!isKnockedBack)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal > 0 && facingDirection < 0 || 
                horizontal < 0 && facingDirection > 0)
            {
                Flip();
            }

            anim.SetFloat("horizontal", Math.Abs(horizontal));
            anim.SetFloat("vertical", Math.Abs(vertical));

            rb.velocity = new Vector2(horizontal,vertical) * speed;
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
    }

    public void KnockedBack(Transform enemy,float knockBackForce,float stunTime)
    {
        isKnockedBack = true;
        Vector2 direction = (transform.position - enemy.transform.position).normalized;
        rb.velocity = direction * knockBackForce;
        StartCoroutine(KnockBackCounter(stunTime));
    }

    private IEnumerator KnockBackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }
}
