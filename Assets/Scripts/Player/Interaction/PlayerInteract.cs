using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : RaycastBehaviour
{
    [SerializeField] private float _checkRate;

    private IInteractable _currentInteractable;

    private KeyCode _interactKeyCode;

    private void Start()
    {
        StartCoroutine(CheckingForInteractable());
        _interactKeyCode = PlayerData.Instance.PlayerInputs.InteractKeyCode;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_interactKeyCode))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        _currentInteractable?.Interact();
    }

    private IEnumerator CheckingForInteractable()
    {
        while (true)
        {
            yield return new WaitForSeconds(_checkRate);
            if (GetHitCollider(out Collider other))
            {
                if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
                {
                    _currentInteractable = interactable;
                    continue;
                }
            }

            _currentInteractable = null;
        }
    }
}