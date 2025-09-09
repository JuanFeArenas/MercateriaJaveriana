using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : Interactable
{
    public int productCount = 0; 
    public ProductMenu productMenu;
    public string productName; 

    public override void Interact(GameObject instance)
    {
        productMenu.ShowMenu(this, productName, instance);
    }

    public override void Interact()
    {
        productMenu.ShowMenu(this, productName, this.gameObject); 
    }
}
