using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaccion : MonoBehaviour
{
    public float interactionDistance = 3f;               
    public KeyCode interactionKey = KeyCode.E;          
    public Text interactionText;                         

    private Camera playerCamera;
    private int layerMask;

    void Start()
    {
        playerCamera = GetComponent<Camera>();

        layerMask = ~(1 << LayerMask.NameToLayer("ProductoVisual"));

        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); 
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, layerMask))   
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null && !ProductMenu.menuActivo)
            {
                if (interactionText != null)
                {
                    interactionText.text = interactable.GetInteractionText();
                    interactionText.gameObject.SetActive(true);
                }

                if (Input.GetKeyDown(interactionKey))
                {
                    interactable.Interact(hit.collider.gameObject);
                }
            }
            else
            {
                OcultarTextoInteraccion();
            }
        }
        else
        {
            OcultarTextoInteraccion();
        }
    }

    public void OcultarTextoInteraccion()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }
}