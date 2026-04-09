using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBow : MonoBehaviour
{
    public Transform launchPoint; 
    public GameObject arrowPrefab;
    private Vector2 aimDirection = Vector2.right;
    public float shootCoolDown = 0.5f;
    private float shootTimer;
    void Update()
    {
        shootTimer -= Time.deltaTime;

        HandleAiming();

        if (Input.GetButtonDown("Shoot") && shootTimer <= 0)
        {
            Shoot();   
        }
    }

    private void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        aimDirection = new Vector2(horizontal,vertical).normalized;
    }

    public void Shoot()
    {
        Arrow arrow = Instantiate(arrowPrefab,launchPoint.position,Quaternion.identity).GetComponent<Arrow>();
        arrow.direction = aimDirection;
        shootTimer = shootCoolDown;
    }
}
