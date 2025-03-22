using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    [SerializeField] private int _id;
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
    public void Interact(int id)
    {
        if _id == id || _id == -1)
        {
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
}
