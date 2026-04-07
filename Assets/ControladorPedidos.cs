using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Plato;
using System.Linq;

public class ControladorPedidos : MonoBehaviour
{
    public static ControladorPedidos instancia { get; private set; }
    private ControladorMenu menu;
    private int numeroTotalComensales;
    private int numeroComensal;
    private const int NUMERO_TOTAL_PLATOS = 5;
    private int numeroPlato;
    private Dictionary<int, List<Plato>> pedidos;
    private string[] textoPlatos = { "el primer plato", " el segundo plato", "el postre", "la bebida", "el café" };
    private string[] platos = { "Primeros", "Segundos", "Postres", "Bebidas", "Cafés" };

    public TextMeshProUGUI textoPaso;
    public TextMeshProUGUI tituloPantalla;
    public TextMeshProUGUI textoComensal;
    public TextMeshProUGUI textoAviso;
    public GameObject panelElegirPlatoComensal;
    public GameObject prefabItemMenu;
    public Transform contenedor;

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(this.gameObject);
            return; // Salimos para que no se ejecute nada más en este objeto "duplicado"
        }
        instancia = this;
        pedidos = new Dictionary<int, List<Plato>>();
        for (int i = 0; i < NUMERO_TOTAL_PLATOS; i++) pedidos[i] = new List<Plato>();
    }

    void Start()
    {
        panelElegirPlatoComensal.SetActive(false);
        textoAviso.enabled = false;
    }


    public void GuardarComensales(int comensales)
    {
        numeroTotalComensales = comensales;
        Debug.Log("Número de comensales guardados: " + numeroTotalComensales );
    }
    public void CargarPanel()
    {
        if (numeroComensal == 0)
        {
            Debug.Log(numeroTotalComensales);
            menu = ControladorMenu.instancia;
            Debug.Log(menu.todosLosPlatos.Count);
            numeroComensal = 1;
            int platoActual = numeroPlato + 1;
            tituloPantalla.text = "Elegir " + textoPlatos[numeroPlato] + " del comensal " + numeroComensal;
            textoPaso.text = "Plato " + platoActual + " de " + NUMERO_TOTAL_PLATOS;
            textoComensal.text = "Comensal " + numeroComensal + " de " + numeroTotalComensales;
        }
    }
    public void ElegirPlato()
    {

        if (numeroComensal <= numeroTotalComensales)
        {
            if (numeroPlato < NUMERO_TOTAL_PLATOS)
            {
                int platoActual = numeroPlato + 1;
                tituloPantalla.text = "Elegir " + textoPlatos[numeroPlato] + " del comensal " + numeroComensal;
                textoPaso.text = "Plato " + platoActual + " de " + NUMERO_TOTAL_PLATOS;
                textoComensal.text = "Comensal " + numeroComensal + " de " + numeroTotalComensales;
                menu.FiltrarPorCategoria(platos[numeroPlato], contenedor, prefabItemMenu);
                Debug.Log("Antes de guardar numPlato:" + numeroPlato + "numeroComensal:" + numeroComensal);

            }
           

        } else
        {
            panelElegirPlatoComensal.SetActive(false);
            //Ir a panel de resumen de pedido
        }
    }

    public void GuardarPlato()
    {
        if (textoAviso.enabled)
        {
            textoAviso.enabled = false;
        }

        ToggleGroup contenedorGrupo = contenedor.GetComponent<ToggleGroup>();
        Toggle toggleActivo = contenedorGrupo.GetFirstActiveToggle();
  
        if (toggleActivo != null)
        {

            // Si quieres el objeto "TextNombre" que está al lado del Toggle
            string nombrePlato = toggleActivo.GetComponentInParent<CargarPlato>().textoNombre.text;

            Plato platoComensal = menu.encontrarPlato(nombrePlato);

            pedidos[numeroPlato].Add(platoComensal);

            numeroPlato++;

            Debug.Log("Plato guardado: " + platoComensal.name.ToString());
            Debug.Log("Guardado plato numPlato:" + numeroPlato + "numeroComensal:" + numeroComensal);

        }
        else
        {
            if (numeroPlato < 3)
            {
                textoAviso.enabled = true;
                Debug.Log("No se ha seleccionado plato numPlato:" + numeroPlato + "numeroComensal:" + numeroComensal);
            }
            else
            {
                numeroPlato++;
            }
        }
        if (numeroPlato == NUMERO_TOTAL_PLATOS)
        {
            numeroPlato = 0;
            numeroComensal++;
            Debug.Log("Reinicio pedido numPlato:" + numeroPlato + "numeroComensal:" + numeroComensal);
            textoAviso.enabled = false;
        }
    }
}

