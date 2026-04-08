using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;
    private Vector2 aimDirection = Vector2.right;
    void Update()
    {
        HandleAiming();
        if (Input.GetButtonDown("Shoot"))
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
        Instantiate(arrowPrefab,launchPoint.position,Quaternion.identity);
    }
}
