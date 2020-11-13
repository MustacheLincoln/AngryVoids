using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hole : MonoBehaviour
{
    public int _force;

    readonly float _maxDragDistance = 2;
    Rigidbody _rigidbody;
    Camera _camera;
    Vector2 _startPosition;

    private Vector3 screenPoint;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
        _startPosition = _rigidbody.position;
    }

    private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = _camera.ScreenToWorldPoint(curScreenPoint) + offset;

        Vector2 desiredPosition = curPosition;
        
        float distance = Vector2.Distance(desiredPosition, _startPosition);
        if (distance > _maxDragDistance)
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + direction * _maxDragDistance;
        }

        if (desiredPosition.x > _startPosition.x)
            desiredPosition.x = _startPosition.x;

        _rigidbody.position = desiredPosition;
    }

    void OnMouseUp()
    {
        Vector2 currentPosition = _rigidbody.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(direction * _force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Grow()
    {
        _rigidbody.useGravity = false;
        _rigidbody.mass += .1f;
        transform.localScale += new Vector3(.1f,.1f,.1f);
    }
}
