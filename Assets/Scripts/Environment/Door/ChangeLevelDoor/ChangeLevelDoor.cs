using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevelDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private int _levelId;

    public void Interact()
    {
        SceneManager.LoadScene(_levelId);
    }
}