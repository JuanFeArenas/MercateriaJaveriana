using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject panelInventario;           
    public Transform contenido;                   
    public GameObject prefabProductoUI;           
    void Start()
    {
        panelInventario.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (panelInventario.activeSelf)
            {
                CerrarInventario();
            }
            else
            {
                AbrirInventario();
            }
        }
    }

    public void AbrirInventario()
    {
        panelInventario.SetActive(true);
        ActualizarInventario();
    }

    public void CerrarInventario()
    {
        panelInventario.SetActive(false);
    }

    public void ActualizarInventario()
    {
        foreach (Transform child in contenido)
        {
            Destroy(child.gameObject);
        }

        foreach (var producto in PlayerInventory.Instancia.productos)
        {
            GameObject item = Instantiate(prefabProductoUI, contenido);
            Text[] textos = item.GetComponentsInChildren<Text>();

            if (textos.Length >= 3)
            {
                textos[0].text = producto.nombre;
                textos[1].text = "Cantidad: " + producto.cantidad;
                textos[2].text = $"Viendo tabla: {producto.tiempoViendoTabla:F2}s";
            }
        }
    }
}