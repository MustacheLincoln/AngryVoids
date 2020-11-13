using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    Rigidbody _rigidbody;

    const float G = 66.74f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Attractor[] attractors = FindObjectsOfType<Attractor>();
        foreach (Attractor attractor in attractors)
        {
            if (attractor != this)
                Attract(attractor);
        }
    }

    void Attract(Attractor objToAttract)
    {
        Rigidbody rigidbodyToAttract = objToAttract._rigidbody;

        Vector3 direction = _rigidbody.position - rigidbodyToAttract.position;
        float distance = direction.magnitude;

        float forceMagnitude = G * (_rigidbody.mass * rigidbodyToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rigidbodyToAttract.AddForce(force);
    }
}
