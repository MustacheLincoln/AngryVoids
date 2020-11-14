using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hole : MonoBehaviour
{
    public int _force;
    public float _growth = .2f;

    readonly float _maxDragDistance = 2;
    Rigidbody _rigidbody;
    Camera _camera;
    Vector2 _startPosition;
    private Object[] _objects;

    public bool IsDragging { get; private set; }

    private Vector3 screenPoint;
    private Vector3 offset;
    public bool _launched = false;
    private Coroutine _cr;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
        _startPosition = _rigidbody.position;
        _objects = FindObjectsOfType<Object>();
    }

    private void OnMouseDown()
    {
        if (_launched == false)
        {
            IsDragging = true;
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    void OnMouseDrag()
    {
        if (_launched == false)
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
    }

    void OnMouseUp()
    {
        if (_launched == false)
        {
            IsDragging = false;
            _launched = true;
            Vector2 currentPosition = _rigidbody.position;
            Vector2 direction = _startPosition - currentPosition;
            direction.Normalize();
            float distance = Vector2.Distance(currentPosition, _startPosition);
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(direction * _force * distance);
            _cr = StartCoroutine(ResetAfterDelay());
            foreach (Object obj in _objects)
                obj.Free();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_launched == true)
        {
            StopCoroutine(_cr);
            _cr = StartCoroutine(ResetAfterDelay());
        }
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Grow()
    {
        if (_launched == true)
            _rigidbody.useGravity = false;
        _rigidbody.mass += _growth;
        transform.localScale += new Vector3(_growth, _growth, _growth);
    }
}
