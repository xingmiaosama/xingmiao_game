using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevation_Enter : MonoBehaviour
{
    public Collider2D[] mountainColliders;
    public Collider2D[] boundaryColliders;
    void Start()
    {
        foreach (Collider2D mountain in mountainColliders)
        {
            mountain.enabled = true;
        }
        foreach (Collider2D boundary in boundaryColliders)
        {
            boundary.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Collider2D mountain in mountainColliders)
            {
                mountain.enabled = false;
            }
            foreach (Collider2D boundary in boundaryColliders)
            {
                boundary.enabled = true;
            }


            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }
    }
}
