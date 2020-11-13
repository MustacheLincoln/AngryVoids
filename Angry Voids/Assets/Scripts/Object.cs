using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Hole hole = collision.gameObject.GetComponent<Hole>();
        if (hole != null)
        {
            Die(hole);
        }
    }

     void Die(Hole hole)
     {
        gameObject.SetActive(false);
        hole.Grow();
     }
}
