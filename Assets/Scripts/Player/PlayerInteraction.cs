using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public Camera MainCam;
    public float interactionDistance = 10f;

    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;
    public TMP_Text stateText;
    void Update()
    {
        InteractionRay();
    }
    public Interactable InteractionRay()
    {
        Ray ray = MainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;

        bool hitSMTH = false;
        Interactable interactable = null;
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                hitSMTH = true;
                interactionText.text = interactable.GetDescription();
                if (interactable is DoorBehaviour DoorState)
                {
                    stateText.text = DoorState.GetState();
                }
                else
                {
                    stateText.text = "";
                }
                if (Input.GetButtonDown("UseItem"))
                {
                    interactable.Interact();
                }
            }

        }
        interactionUI.SetActive(hitSMTH);
        stateText.gameObject.SetActive(hitSMTH);
        return interactable;
    }
}
