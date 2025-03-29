using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable 
{
    void Interact(int id = -1);
    string GetDescription();
}
