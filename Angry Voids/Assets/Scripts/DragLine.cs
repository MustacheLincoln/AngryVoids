using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragLine : MonoBehaviour
{
    LineRenderer _lineRenderer;
    Hole _hole;
    

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _hole = FindObjectOfType<Hole>();
        _lineRenderer.SetPosition(0, _hole.transform.position);
    }

    void Update()
    {
        if (_hole.IsDragging)
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(1, _hole.transform.position);
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }
        
}
