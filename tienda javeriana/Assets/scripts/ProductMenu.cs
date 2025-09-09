using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ProductMenu : MonoBehaviour
{
    private GameObject productInstance;
    public GameObject menuPanel; 
    public Button addProductButton;
    public Button returnProductButton;
    public Button showNutritionButton; 
    public Button closeMenuButton;
    public Button saveDataButton; 
    public Image nutritionTableImage; 
    public AudioSource addProductSound; 
    public static bool menuActivo = false;


    public movimientopriemrapersona camaraPrimeraPersona;
    private Menu currentProduct; 
    private string selectedProduct; 
    private bool isMenuOpen = false;
    private ProductBox currentBox; 
    public Interaccion interaccion;

    
    private float nutritionOpenTime = 0f; 
    private float nutritionViewTime = 0f; 

    private static ProductMenu activeNutritionMenu = null;

    void Start()
    {
        
        menuPanel.SetActive(false);
        nutritionTableImage.gameObject.SetActive(false);

        
        addProductButton.onClick.AddListener(OnAddProduct);
        returnProductButton.onClick.AddListener(OnReturnProduct);
        showNutritionButton.onClick.AddListener(OnShowNutrition);
        closeMenuButton.onClick.AddListener(OnCloseMenu);
        saveDataButton.onClick.AddListener(SaveProductDataToFile); 
    }

   
    public void ShowMenu(Menu product, string productName, GameObject instance)
{
    currentProduct = product;
    selectedProduct = productName;
    productInstance = instance;

    menuPanel.SetActive(true);
    isMenuOpen = true;

    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
}

    
    public void HideMenu()
    {
        menuActivo = false;
        menuPanel.SetActive(false);
        isMenuOpen = false;

        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (camaraPrimeraPersona != null)
    {
        camaraPrimeraPersona.enabled = true;
    }
    }

    
    private void OnAddProduct()
    {
        if (currentBox != null && productInstance != null)
        {
            currentBox.ConsumirProductoVisual(productInstance);

            if (addProductSound != null)
                addProductSound.Play();
        }

        HideMenu();
    }

    private void OnReturnProduct()
    {
        if (currentBox != null && PlayerInventory.Instancia.TieneProducto(selectedProduct))
        {
        PlayerInventory.Instancia.EliminarProducto(selectedProduct);

        currentBox.RecibirProductoVisual();

        Debug.Log($"Producto {selectedProduct} devuelto a la caja.");
        }
        else
        {
            Debug.Log("No tienes productos para devolver.");
        }

        HideMenu();
    }

    
    private void OnShowNutrition()
    {
        
        if (activeNutritionMenu != null && activeNutritionMenu != this)
        {
            activeNutritionMenu.CloseNutritionTable();
        }

        
        activeNutritionMenu = this;

       
        nutritionOpenTime = Time.time;
        
       
        isMenuOpen = menuPanel.activeSelf;
        nutritionTableImage.gameObject.SetActive(true);
        
        
    }


    private void OnCloseMenu()
    {
        if (nutritionTableImage.gameObject.activeSelf)
        {
            CloseNutritionTable();
        }
        else
        {
            HideMenu();
        }
        if (camaraPrimeraPersona != null)
        {
            camaraPrimeraPersona.enabled = true;
        }
        HideMenu();
    }

    
    public void CloseNutritionTable()
    {
        if (nutritionTableImage.gameObject.activeSelf)
        {
            
            nutritionViewTime += Time.time - nutritionOpenTime;
            Debug.Log($"Tiempo total viendo la tabla de {selectedProduct}: {nutritionViewTime} segundos");
        }
        nutritionTableImage.gameObject.SetActive(false);
        
    }

    
    private void SaveProductDataToFile()
    {
        string path = Application.persistentDataPath + "/ProductData.txt"; 
        string data = $"Producto: {selectedProduct}\nCantidad: {currentProduct.productCount}\nTiempo de tabla: {nutritionViewTime} segundos\n\n";

        File.AppendAllText(path, data); 

        Debug.Log("Datos guardados en: " + path);
    }
    
public string GetSelectedProduct()
{
    return selectedProduct;
}


public int GetProductCount()
{
    return currentProduct != null ? currentProduct.productCount : 0;
}


public float GetNutritionViewTime()
{
    return nutritionViewTime;
}

    public void ShowMenuFromBox(ProductBox box, string productName, GameObject instance, Sprite tablaNutricional)
    {
        if (interaccion != null)
        {
            interaccion.OcultarTextoInteraccion();
        }
        menuActivo = true;
        currentBox = box;
        selectedProduct = productName;
        productInstance = instance;
        

        menuPanel.SetActive(true);
        isMenuOpen = true;

        returnProductButton.interactable = !box.EstaLlena();

        if (nutritionTableImage != null && tablaNutricional != null)
        {
            nutritionTableImage.sprite = tablaNutricional;
        }

        if (addProductButton != null)
        {
            addProductButton.interactable = box.HayProductosDisponibles();
        }
        

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (camaraPrimeraPersona != null)
        {
            camaraPrimeraPersona.enabled = false;
        }

        interaccion.OcultarTextoInteraccion();

        

    
    
    
    }

}
