using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDragAndDrop : RaycastBehaviour
{
    [SerializeField] private Transform _dragPoint;
    [SerializeField] private float _throwForce;

    private IDragDropped _currentDragDropped;

    private bool _pickuped;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            TryPickup();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TryDrop();
        }

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            TryThrow();
        }
        
        if (GetHitCollider(out Collider other))
        {
            if (other.TryGetComponent<IDragDropped>(out IDragDropped dragDropped))
            {
                _currentDragDropped = dragDropped;
                return;
            }
        }

        if (!_pickuped)
            _currentDragDropped = null;
    }

    public void TryPickup()
    {
        if (_currentDragDropped == null)
            return;


        if (!_currentDragDropped.Pickuped)
        {
            _pickuped = true;
            _currentDragDropped.Pickup(_dragPoint);
        }
    }

    public void TryThrow()
    {
        if (_currentDragDropped == null)
            return;

        if (_currentDragDropped.Pickuped)
        {
            _currentDragDropped.Throw(Camera.forward * _throwForce);
            _pickuped = false;
        }
    }
    
    public void TryDrop()
    {
        if (_currentDragDropped == null)
            return;

        if (_currentDragDropped.Pickuped)
        {
            _currentDragDropped.Drop();
            _pickuped = false;
        }
    }
}