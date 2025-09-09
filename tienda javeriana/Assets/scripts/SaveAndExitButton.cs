using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System; 

public class SaveAndExitButton : MonoBehaviour
{
    private bool isPlayerNear = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerNear = false;
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E)) 
        {
            SaveProductDataToCSV();
            Application.Quit(); 
            Debug.Log("Aplicación cerrada.");
        }
    }

    private void SaveProductDataToCSV()
    {
        ProductMenu[] allMenus = FindObjectsOfType<ProductMenu>(); 
        if (allMenus.Length == 0)
        {
            Debug.LogWarning("No hay productos para guardar.");
            return;
        }

        string downloadPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile), "Downloads");
        string filePath = Path.Combine(downloadPath, "Resumen_Productos.csv");

        List<ProductMenu> interactedProducts = new List<ProductMenu>();

        foreach (ProductMenu menu in allMenus)
        {
            if (menu.GetProductCount() > 0 || menu.GetNutritionViewTime() > 0) 
            {
                interactedProducts.Add(menu);
            }
        }

        if (interactedProducts.Count == 0) 
        {
            Debug.LogWarning("No hay productos con los que se haya interactuado.");
            return;
        }

        using (StreamWriter writer = new StreamWriter(filePath, true)) 
        {
            writer.Write("Usuario"); 
            foreach (ProductMenu menu in interactedProducts)
            {
                string productName = menu.GetSelectedProduct();
                int productCount = menu.GetProductCount();
                string nutritionTime = $"{menu.GetNutritionViewTime():F2}s"; 

                writer.Write($", Cantidad: {productName} ({productCount}), Tiempo visto tabla nutricional: \"{nutritionTime}\"");
            }

            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            writer.Write($", {dateTime}");
            writer.WriteLine();
        }

        Debug.Log("CSV actualizado con los productos interactuados en: " + filePath);
    }
}
