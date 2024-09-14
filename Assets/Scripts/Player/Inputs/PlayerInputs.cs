using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Input")]
public class PlayerInputs : ScriptableObject
{
    public KeyCode InteractKeyCode;

    [Space(10)] [Header("Drag&Drop")] public KeyCode PickupObjectKeyCode;
    public KeyCode DropObjectKeyCode;
    public KeyCode ThrowObjectKeyCode;
}