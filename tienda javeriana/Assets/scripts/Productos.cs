using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptProducto : MonoBehaviour
{
    public string nombreProducto;
    public string tablaNutricional;
    public Sprite imagenProducto;
    public float tiempoViendoTabla;
    
    private float tiempoInicioTabla;

    public void VerTabla()
    {
        tiempoInicioTabla = Time.time;
        // Mostrar UI con tablaNutricional
    }

    public void CerrarTabla()
    {
        tiempoViendoTabla += Time.time - tiempoInicioTabla;
        // Ocultar UI
    }
}
