using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ControladorResumen : MonoBehaviour
{
    public static ControladorResumen instancia { get; private set; }
    public GameObject contenedor;
    public GameObject prefab;
    public GameObject panelResumenPedido;
    public GameObject panelRonda;
    private Dictionary<int, List<Plato>> pedidos;


    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(this.gameObject);
            return; // Salimos para que no se ejecute nada más en este objeto "duplicado"
        }
        instancia = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        panelResumenPedido.SetActive(false);
        
    }

    public void CargarResumenPedido(Dictionary<int, List<Plato>> pedidos)
    {
        this.pedidos = pedidos;
        // Limpiamos el contenedor por si acaso para no duplicar datos
        foreach (Transform child in contenedor.transform)
        {
            Destroy(child.gameObject);
        }

        // Comprobamos el número de comensales basándonos en los platos del primer tiempo
        // Verificamos si existe la categoría 0 (primeros platos)
        if (pedidos == null || !pedidos.ContainsKey(0)) 
        {
            Debug.LogWarning("No hay pedidos registrados en la categoría 0 (Primeros).");
            return;
        }

        int numComensales = pedidos[0].Count;

        for (int i = 0; i < numComensales; i++)
        {
            // Instanciar el prefab de resumen de cada comensal
            GameObject nuevoPanel = Instantiate(prefab, contenedor.transform);
            
            // CORRECCIÓN DE LAYOUT: Resetear RectTransform para que el LayoutGroup lo maneje bien
            RectTransform rt = nuevoPanel.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition = Vector2.zero;
                rt.localScale = Vector3.one;
                Debug.Log("Panel Comensal " + (i+1) + " instanciado en pos: " + rt.anchoredPosition);
            }

            // Aseguramos que ImageComensal esté activo (es un icono estático)
            Transform tImageComensal = nuevoPanel.transform.Find("ImageComensal");
            if (tImageComensal != null) 
            {
                tImageComensal.gameObject.SetActive(true);
            }

            // Actualizamos el número de comensal en el texto
            // Buscamos cualquier componente TextMeshProUGUI en los hijos, independientemente del nombre
            TextMeshProUGUI textoComensal = nuevoPanel.GetComponentInChildren<TextMeshProUGUI>();
            if (textoComensal != null)
            {
                textoComensal.text = "Comensal " + (i + 1);
            }
            else
            {
                Debug.LogWarning("No se encontró ningún componente de texto (TextMeshProUGUI) en el prefab '" + nuevoPanel.name + "'.");
            }
            

            // Platos obligatorios (0: Primero, 1: Segundo, 2: Postre)
            ActualizarImagenPlato(nuevoPanel, "ImgPrimero", pedidos, 0, i);
            ActualizarImagenPlato(nuevoPanel, "ImgSegundo", pedidos, 1, i);
            ActualizarImagenPlato(nuevoPanel, "ImgPostre", pedidos, 2, i);

            // Platos opcionales (3: Bebida, 4: Cafe)
            ActualizarImagenPlato(nuevoPanel, "ImgBebida", pedidos, 3, i, true);
            ActualizarImagenPlato(nuevoPanel, "ImgCafe", pedidos, 4, i, true);
        }
    }

    private void ActualizarImagenPlato(GameObject panel, string nombreObjeto, Dictionary<int, List<Plato>> platos, int categoria, int indexComensal, bool opcional = false)
    {
        // Buscamos el objeto por nombre dentro del panel
        Transform t = panel.transform.Find(nombreObjeto);
        if (t == null) return;

        Image img = t.GetComponent<Image>();
        if (img == null) return;

        // Verificamos si existe el plato para esta categoría e índice de comensal
        if (platos.ContainsKey(categoria) && indexComensal < platos[categoria].Count && platos[categoria][indexComensal] != null)
        {
            img.sprite = platos[categoria][indexComensal].imagenPlato;
            t.gameObject.SetActive(true);
        }
        else
        {
            // Si es opcional y no hay plato, ocultamos el objeto de imagen
            if (opcional)
            {
                t.gameObject.SetActive(false);
            }
        }
    }

    public void PedirPedidos()
    {
        panelResumenPedido.SetActive(false);
        panelRonda.SetActive(true);
        ControladorRonda.instancia.IniciarRondas(pedidos);
    }
}
