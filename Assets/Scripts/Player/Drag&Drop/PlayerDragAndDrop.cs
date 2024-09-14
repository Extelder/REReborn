using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDragAndDrop : RaycastBehaviour
{
    [SerializeField] private Transform _dragPoint;
    [SerializeField] private float _throwForce;
    [SerializeField] private float _resetDropDelay;

    private IDragDropped _currentDragDropped;

    private bool _pickuped;
    private bool _canDrop;

    private KeyCode _pickupKeyCode;
    private KeyCode _dropKeyCode;
    private KeyCode _throwKeyCode;

    private void Start()
    {
        _pickupKeyCode = PlayerData.Instance.PlayerInputs.PickupObjectKeyCode;
        _dropKeyCode = PlayerData.Instance.PlayerInputs.DropObjectKeyCode;
        _throwKeyCode = PlayerData.Instance.PlayerInputs.ThrowObjectKeyCode;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_dropKeyCode))
        {
            if ((_canDrop))
                TryDrop();
            Debug.Log(_canDrop);
            Debug.Log(_currentDragDropped.Pickuped);
        }

        if (Input.GetKeyDown(_pickupKeyCode))
        {
            TryPickup();
        }


        if (Input.GetKeyDown(_throwKeyCode))
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
            Invoke(nameof(ResetDrop), _resetDropDelay);
            _pickuped = true;
            _currentDragDropped.Pickup(_dragPoint);
        }
    }

    private void ResetDrop()
    {
        _canDrop = true;
    }

    public void TryThrow()
    {
        if (_currentDragDropped == null)
            return;

        _canDrop = false;
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

        _canDrop = false;
        if (_currentDragDropped.Pickuped)
        {
            _currentDragDropped.Drop();
            _pickuped = false;
        }
    }
}