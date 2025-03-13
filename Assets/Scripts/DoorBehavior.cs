using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    public Animator _animator;
    public bool IsOpen;
    void Start()
    {
        if (IsOpen)
        {
            _animator.SetBool("IsOpen", true);
        }
    }
    public string GetDescription()
    {
        if (IsOpen) return "Press E to close the door";
        return "Press E to open the door";
    }
    public void Interact()
    {
        IsOpen = !IsOpen;
        if (IsOpen)
        {
            _animator.SetBool("IsOpen", true);
        }
        else
        {
            _animator.SetBool("IsOpen", false);
        }

    }
}
