using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.right;
    public float speed;
    public float lifeTime = 2f;

    void Start()
    {
        rb.velocity = direction * speed;
        Destroy(gameObject,lifeTime);
    }
}
