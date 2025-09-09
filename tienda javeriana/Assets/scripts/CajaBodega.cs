using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CajaBodega : Interactable
{
    public string nombreProducto = "agua";  
    public int cantidadDisponible = 20;       
    public int cantidadPorRetiro = 5;         

    public override void Interact()
    {
        if (cantidadDisponible >= cantidadPorRetiro)
        {
            cantidadDisponible -= cantidadPorRetiro;

            for (int i = 0; i < cantidadPorRetiro; i++)
            {
                PlayerInventory.Instancia.AgregarProducto(nombreProducto, 0f);
            }

            Debug.Log($"Retiraste {cantidadPorRetiro} unidades de {nombreProducto}. Quedan {cantidadDisponible}.");
        }
        else
        {
            Debug.Log("No hay suficientes unidades en la caja.");
        }
    }
}
