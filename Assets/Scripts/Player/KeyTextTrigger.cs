using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTextTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _gameobject;

    private void OnTriggerEnter(Collider colider)
    {
        if (colider.tag == "Player")
        {
            _gameobject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider colider)
    {
        if (colider.tag == "Player")
        {
            _gameobject.SetActive(false);
        }
    }
}
