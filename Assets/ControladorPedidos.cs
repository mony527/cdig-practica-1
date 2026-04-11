using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Plato;
using System.Linq;
using Unity.VisualScripting;

public class ControladorPedidos : MonoBehaviour
{
    public static ControladorPedidos instancia { get; private set; }
    private ControladorMenu menu;
    private int numeroTotalComensales;
    private int numeroComensal;
    private const int NUMERO_TOTAL_PLATOS = 5;
    private int numeroPlato;
    private Dictionary<int, List<Plato>> pedidos { get; set; }
    private string[] textoPlatos = { "el primer plato", " el segundo plato", "el postre", "la bebida", "el café" };
    private string[] platos = { "Primeros", "Segundos", "Postres", "Bebidas", "Cafés" };

    public TextMeshProUGUI textoPaso;
    public TextMeshProUGUI tituloPantalla;
    public TextMeshProUGUI textoComensal;
    public GameObject panelElegirPlatoComensal;
    public GameObject panelResumenPedido;
    public GameObject prefabItemMenu;
    public Transform contenedor;
    public Button botonSiguiente;
    public Button botonAtras;

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(this.gameObject);
            return; 
        }
        instancia = this;
        pedidos = new Dictionary<int, List<Plato>>();
        for (int i = 0; i < NUMERO_TOTAL_PLATOS; i++) pedidos[i] = new List<Plato>();
    }


    void Start()
    {
        panelElegirPlatoComensal.SetActive(false);
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
        if(numeroPlato == 0)
        {
            botonAtras.gameObject.SetActive(false);
        }
        else
        {
            botonAtras.gameObject.SetActive(true);
        }

        if(numeroPlato == 4)
        {
            botonSiguiente.GetComponentInChildren<TMP_Text>().text = "Listo";
        }
        else
        {
            botonSiguiente.GetComponentInChildren<TMP_Text>().text = "Siguiente";
        }

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
                if (numeroPlato >= 0 && numeroPlato < 3)
                {
                    botonSiguiente.interactable = false;

                }
            }

        }
        else
        {
            panelElegirPlatoComensal.SetActive(false);
            panelResumenPedido.SetActive(true);
            ControladorResumen.instancia.CargarResumenPedido(pedidos);
        }
    }

    public void ActivarBotonSiguiente()
    {

        if (numeroPlato >= 0 && numeroPlato <= 2)
        {
            ToggleGroup contenedorGrupo = contenedor.GetComponent<ToggleGroup>();
            bool haySeleccionado = contenedorGrupo.AnyTogglesOn();

            botonSiguiente.interactable = haySeleccionado;

        }
    }

    public void GuardarPlato()
    {

        ToggleGroup contenedorGrupo = contenedor.GetComponent<ToggleGroup>();
        Toggle toggleActivo = contenedorGrupo.GetFirstActiveToggle();
  
        if (toggleActivo != null)
        {

            
            string nombrePlato = toggleActivo.GetComponentInParent<CargarPlato>().textoNombre.text;

            Plato platoComensal = menu.encontrarPlato(nombrePlato);

            pedidos[numeroPlato].Add(platoComensal);

            numeroPlato++;

            Debug.Log("Plato guardado: " + platoComensal.name.ToString());
            Debug.Log("Guardado plato numPlato:" + numeroPlato + "numeroComensal:" + numeroComensal);

        }
        else
        {
            if (numeroPlato > 2)
            {
                pedidos[numeroPlato].Add(null);
                numeroPlato++;
            }
        }
        if (numeroPlato == NUMERO_TOTAL_PLATOS)
        {
            numeroPlato = 0;
            numeroComensal++;
            Debug.Log("Reinicio pedido numPlato:" + numeroPlato + "numeroComensal:" + numeroComensal);
        }
    }

    public void IrAtras()
    {
        if (numeroPlato > 0 && numeroPlato < NUMERO_TOTAL_PLATOS)
        {
            pedidos[numeroPlato-1].RemoveAt(numeroComensal-1);
            numeroPlato--;
        }
    }

    public void Reiniciar()
    {
        pedidos = new Dictionary<int, List<Plato>>();
        for (int i = 0; i < NUMERO_TOTAL_PLATOS; i++) pedidos[i] = new List<Plato>();
        numeroComensal = 0;
        numeroPlato = 0;
    }
}

