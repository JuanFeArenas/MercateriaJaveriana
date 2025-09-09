using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable  : MonoBehaviour
{
    public string interactionMessage = "Presiona E para interactuar";

    public virtual string GetInteractionText()
    {
        return interactionMessage;
    }

    public abstract void Interact();

    public virtual void Interact(GameObject instance)
    {
        Interact();
    }
}
