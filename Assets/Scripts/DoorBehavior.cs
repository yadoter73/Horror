using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour, Interactable
{
    [SerializeField] private bool locked;
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
            return "Press F to close the door";
        }
        return "Press F to open the door";
    }
    public void Interact(int id)
    {
        if (locked)
        {
            return;
        }
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
    public void ToggleLock(int id)
    {
        if (id == _id)
            locked = !locked;
    }




}
