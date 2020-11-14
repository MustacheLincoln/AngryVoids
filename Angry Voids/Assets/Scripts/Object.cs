using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    Animator _animator;
    private bool _dead = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Hole hole = collision.gameObject.GetComponent<Hole>();
        if (hole != null)
        {
            if (_dead == false)
                Die(hole);
        }
    }

     void Die(Hole hole)
     {
        _dead = true;
        _animator.enabled = true;
        hole.Grow();
        Destroy(gameObject, .2f);
     }
}
