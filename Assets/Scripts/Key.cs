using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Key : Item
{
    [SerializeField] private int id;
    private PlayerInteraction _obj;
    public override void Use(GameObject user, IInventory inventory)
    {
        Interactable a = _obj.InteractionRay();
        if (a != null)
        {
            if (a is DoorBehaviour doorBehaviour)
            {
                doorBehaviour.ToggleLock(id);
            }
        }

    }
    private void Start()
    {
        _obj = FindAnyObjectByType<PlayerInteraction>();
    }
}
