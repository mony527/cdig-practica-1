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
    public Sprite imagenXSinPlato;

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(this.gameObject);
            return; 
        }
        instancia = this;
    }


    
    void Start()
    {
        panelResumenPedido.SetActive(false);
        
    }

    public void CargarResumenPedido(Dictionary<int, List<Plato>> pedidos)
    {
        this.pedidos = pedidos;
        foreach (Transform child in contenedor.transform)
        {
            Destroy(child.gameObject);
        }

        
        if (pedidos == null || !pedidos.ContainsKey(0)) 
        {
            Debug.LogWarning("No hay pedidos registrados en la categoría 0 (Primeros).");
            return;
        }

        int numComensales = pedidos[0].Count;

        for (int i = 0; i < numComensales; i++)
        {
            
            GameObject nuevoPanel = Instantiate(prefab, contenedor.transform);
            
           
            RectTransform rt = nuevoPanel.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition = Vector2.zero;
                rt.localScale = Vector3.one;
                Debug.Log("Panel Comensal " + (i+1) + " instanciado en pos: " + rt.anchoredPosition);
            }

            
            Transform tImageComensal = nuevoPanel.transform.Find("ImageComensal");
            if (tImageComensal != null) 
            {
                tImageComensal.gameObject.SetActive(true);
            }

            
            TextMeshProUGUI textoComensal = nuevoPanel.GetComponentInChildren<TextMeshProUGUI>();
            if (textoComensal != null)
            {
                textoComensal.text = "Comensal " + (i + 1);
            }
            else
            {
                Debug.LogWarning("No se encontró ningún componente de texto (TextMeshProUGUI) en el prefab '" + nuevoPanel.name + "'.");
            }
            

            
            ActualizarImagenPlato(nuevoPanel, "ImgPrimero", pedidos, 0, i);
            ActualizarImagenPlato(nuevoPanel, "ImgSegundo", pedidos, 1, i);
            ActualizarImagenPlato(nuevoPanel, "ImgPostre", pedidos, 2, i);

            
            ActualizarImagenPlato(nuevoPanel, "ImgBebida", pedidos, 3, i, true);
            ActualizarImagenPlato(nuevoPanel, "ImgCafe", pedidos, 4, i, true);
        }
    }

    private void ActualizarImagenPlato(GameObject panel, string nombreObjeto, Dictionary<int, List<Plato>> platos, int categoria, int indexComensal, bool opcional = false)
    {
        
        Transform t = panel.transform.Find(nombreObjeto);
        if (t == null) return;

        Image img = t.GetComponent<Image>();
        if (img == null) return;

        
        if (platos.ContainsKey(categoria) && indexComensal < platos[categoria].Count && platos[categoria][indexComensal] != null)
        {
            img.sprite = platos[categoria][indexComensal].imagenPlato;
            t.gameObject.SetActive(true);
        }
        else
        {
            
            if (opcional)
            {
                img.sprite = imagenXSinPlato;
                t.gameObject.SetActive(true);
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
