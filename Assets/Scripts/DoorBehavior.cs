using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    public Animator animator;
    public bool IsOpen;
    void Start()
    {
        animator = GetComponent<Animator>();
        if (IsOpen)
        {
            animator.SetBool("IsOpen", true);
        }
    }
    public string GetDescription()
    {
        if (IsOpen)
        {
            return "Press E to close the door";
        }
        return "Press E to open the door";
    }
    public void Interact()
    {
        //IsOpen = !IsOpen
        IsOpen = !IsOpen;
        if (IsOpen)
        {
            animator.SetBool("IsOpen", true);
        }
        else
        {
            animator.SetBool("IsOpen", false);
        }

    }
}
