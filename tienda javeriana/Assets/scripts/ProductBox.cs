using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProductBox : Interactable
{
    [System.Serializable]
    public class Producto
    {
        public string nombre;
        public Sprite imagenNutricional;
    }

    [System.Serializable]
    public class ProductoInstanciaOriginal
    {
        public GameObject prefab;
        public Vector3 posicion;
        public Quaternion rotacion;

        public ProductoInstanciaOriginal(GameObject prefab, Vector3 posicion, Quaternion rotacion)
        {
            this.prefab = prefab;
            this.posicion = posicion;
            this.rotacion = rotacion;
        }
    }
    [System.Serializable]
    public class ProductoVisual
    {
        public GameObject prefab;          
        public Transform spawnPoint;         
        [HideInInspector] public GameObject instanciaActual; 
    }

    public Producto producto;

    public ProductMenu productMenu; 

    public List<ProductoVisual> productosConfigurados = new List<ProductoVisual>();

    private Queue<ProductoInstanciaOriginal> productosOriginales = new Queue<ProductoInstanciaOriginal>();
    private List<GameObject> productosFisicos = new List<GameObject>();


    void Start()
    {
        productosFisicos.Clear();
        productosOriginales.Clear();

        foreach (ProductoVisual config in productosConfigurados)
        {
            if (config.prefab == null || config.spawnPoint == null)
            {
                Debug.LogWarning($"Faltan referencias en un producto visual en la caja {gameObject.name}");
                continue;
            }

            GameObject nuevo = Instantiate(config.prefab, config.spawnPoint.position, config.spawnPoint.rotation);

            nuevo.transform.localScale = config.prefab.transform.localScale;

            productosOriginales.Enqueue(new ProductoInstanciaOriginal(
                config.prefab,
                config.spawnPoint.position,
                config.spawnPoint.rotation
            ));

            productosFisicos.Add(nuevo);
        }

        Debug.Log($"Instanciados {productosFisicos.Count} productos en la caja {gameObject.name}");
    }

    public override void Interact(GameObject instance)
    {
        GameObject productoInstancia = productosFisicos.Count > 0 ? productosFisicos[0] : null;

        if (productMenu != null)
        {
            productMenu.ShowMenuFromBox(this, producto.nombre, productoInstancia, producto.imagenNutricional);
        }
    }

    public override void Interact() { }

    public void RecibirProductoVisual()
    {
        productosFisicos.RemoveAll(obj => obj == null);

        ProductoInstanciaOriginal[] originales = productosOriginales.ToArray();

        for (int i = 0; i < originales.Length; i++)
        {
            var original = originales[i];
            bool yaExiste = false;

            foreach (var obj in productosFisicos)
            {
                if (obj == null) continue; 
                if (Vector3.Distance(obj.transform.position, original.posicion) < 0.1f)
                {
                    yaExiste = true;
                    break;
                }
            }

            if (!yaExiste)
            {
                GameObject nuevo = Instantiate(original.prefab, original.posicion, original.rotacion);
                productosFisicos.Add(nuevo);
                return;
            }
        }

        Debug.LogWarning("No hay productos para devolver.");
    }
    public void ConsumirProductoVisual(GameObject instancia)
    {
        instancia.name = "INSTANCIA A DESTRUIR";
        if (productosFisicos.Contains(instancia))
        {
            productosFisicos.Remove(instancia);
            Destroy(instancia);

            PlayerInventory.Instancia.AgregarProducto(producto.nombre, productMenu.GetNutritionViewTime());
            Debug.Log($"Producto agregado: {producto.nombre}. Restantes: {productosFisicos.Count}");
        }
        else
        {
            Debug.LogWarning("La instancia no pertenece a esta caja.");
        }
    }

    public bool HayProductosDisponibles()
    {
        return productosFisicos.Count > 0;
    }
    
    public bool EstaLlena()
    {
        productosFisicos.RemoveAll(obj => obj == null);

        return productosFisicos.Count >= productosOriginales.Count;
    }
}