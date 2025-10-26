using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("移动参数")]
    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;
    public bool canDoubleJump = false;

    [Header("物理参数")]
    public float gravityScale = 3.0f;
    public float momentum = 0.1f;
}
