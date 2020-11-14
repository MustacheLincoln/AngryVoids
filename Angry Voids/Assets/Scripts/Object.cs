using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    Animator _animator;
    private Hole _hole;
    private bool _dead = false;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _hole = FindObjectOfType<Hole>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Hole hole = collision.gameObject.GetComponent<Hole>();
        if (hole != null)
        {
            if (_dead == false)
                Die();
        }
    }

     void Die()
     {
        _dead = true;
        _animator.enabled = true;
        _hole.Grow();
        Destroy(gameObject, .2f);
     }

    public void Free()
    {
        _rigidbody.isKinematic = false;
    }
}
