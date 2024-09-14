using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [field: SerializeField] public PlayerInputs PlayerInputs { get; private set; }

    public static PlayerData Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            return;
        }

        Debug.LogError(
            "Игорь блядь в сцене не должно быть PlayerData, и он идет с 0 левела через DontDestroyOnLoad Сукаааа....");
        Destroy(gameObject);
    }
}