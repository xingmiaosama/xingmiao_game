using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameMainProgram : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
