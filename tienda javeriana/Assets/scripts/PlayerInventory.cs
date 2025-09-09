using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProductoInfo
{
    public string nombre;
    public int cantidad;
    public float tiempoViendoTabla;
}

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instancia;
    public List<ProductoInfo> productos = new List<ProductoInfo>();

    private void Awake()
    {
        if (Instancia == null) Instancia = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            MostrarInventario();
        }
    }

    public void AgregarProducto(string nombre, float tiempoTabla)
    {
        var producto = productos.Find(p => p.nombre == nombre);
        if (producto != null)
        {
            producto.cantidad++;
            producto.tiempoViendoTabla += tiempoTabla;
        }
        else
        {
            productos.Add(new ProductoInfo
            {
                nombre = nombre,
                cantidad = 1,
                tiempoViendoTabla = tiempoTabla
            });
        }
    }

    
    public bool TieneProducto(string nombre)
    {
        var p = productos.Find(x => x.nombre == nombre);
        return p != null && p.cantidad > 0;
    }

    public void EliminarProducto(string nombre)
    {
        var p = productos.Find(x => x.nombre == nombre);
        if (p != null && p.cantidad > 0)
        {
         p.cantidad--;
        }
    }

    public void MostrarInventario()
    {
        Debug.Log("----- INVENTARIO -----");
        foreach (var p in productos)
        {
            Debug.Log($"Producto: {p.nombre} | Cantidad: {p.cantidad} | Tiempo viendo tabla: {p.tiempoViendoTabla:F2} s");
        }
        Debug.Log("----------------------");
    }
}