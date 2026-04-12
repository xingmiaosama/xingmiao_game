using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBow : MonoBehaviour
{
    public Transform launchPoint; 
    public GameObject arrowPrefab;
    private Vector2 aimDirection = Vector2.right;
    public Animator anim;
    public PlayerMovement playerMovement;
    public float shootCoolDown = 0.5f;
    private float shootTimer;
    void Update()
    {
        shootTimer -= Time.deltaTime;

        HandleAiming();

        if (Input.GetButtonDown("Shoot") && shootTimer <= 0)
        {
            anim.SetBool("isShooting",true);
            playerMovement.isShooting = true;
        }
    }

    private void OnEnable()
    {
        anim.SetLayerWeight(0,0);
        anim.SetLayerWeight(1,1);
    }

    private void OnDisable()
    {
        anim.SetLayerWeight(0,1);
        anim.SetLayerWeight(1,0);
    }

    private void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if(horizontal != 0 || vertical != 0)
        {
            aimDirection = new Vector2(horizontal,vertical).normalized;
            anim.SetFloat("aimX",aimDirection.x);
            anim.SetFloat("aimY",aimDirection.y);
        }
    }

    public void Shoot()
    {
        if(shootTimer <= 0)
        {
        Arrow arrow = Instantiate(arrowPrefab,launchPoint.position,Quaternion.identity).GetComponent<Arrow>();
        arrow.direction = aimDirection;
        shootTimer = shootCoolDown;
        }

        anim.SetBool("isShooting",false);
        playerMovement.isShooting = false;
    }
}
