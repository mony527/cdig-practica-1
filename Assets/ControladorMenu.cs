using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; // Para filtrar fácilmente
using TMPro;


public class ControladorMenu : MonoBehaviour
{
    public GameObject panelMenu;
    public GameObject panelElegirPlatoComensal;
    public GameObject panelInfoPlato;

    private ControladorInfoPlato controladorInfoPlato;

    public List<Plato>todosLosPlatos; // Arrastra aquí todos tus archivos .asset
    public GameObject prefabItemMenu; // Tu prefab 'ButtonPlano'
    public Transform contenedor; // El objeto con el 'Grid Layout Group'

    public TMP_Dropdown opcionFiltro;

    public static ControladorMenu instancia { get; private set; }

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
        panelMenu.SetActive(false);
        MostrarTodos();
    }

    public void HacerPedido()
    {
        panelMenu.SetActive(false);
        panelElegirPlatoComensal.SetActive(true);
        ControladorPedidos.instancia.CargarPanel();
        ControladorPedidos.instancia.ElegirPlato();
    }
    
    public void MostrarTodos() => CargarMenu(todosLosPlatos);

    public void FiltrarPorCategoria()
    {
        string categoriaString = opcionFiltro.options[opcionFiltro.value].text;

        if (categoriaString.Equals("Seleccione un filtro de platos"))
        {
            CargarMenu(todosLosPlatos);
        }
        else
        {
            categoriaString = categoriaString.Substring(0, categoriaString.Length - 1);
            TipoPlato cat = (TipoPlato)System.Enum.Parse(typeof(TipoPlato), categoriaString);
            var filtrados = todosLosPlatos.Where(p => p.tipo == cat).ToList();
            CargarMenu(filtrados);

        }
    }

    public void FiltrarPorCategoria(string categoria, Transform contenedor, GameObject prefabItemMenu)
    {
        categoria = categoria.Substring(0, categoria.Length - 1);
        TipoPlato cat = (TipoPlato)System.Enum.Parse(typeof(TipoPlato), categoria);
        var filtrados = todosLosPlatos.Where(p => p.tipo == cat).ToList();
        CargarMenu(filtrados, contenedor, prefabItemMenu);
    }

    void CargarMenu(List<Plato> listaAMostrar)
    {
        foreach (Transform hijo in contenedor) Destroy(hijo.gameObject);

        foreach (Plato plato in listaAMostrar)
        {
            GameObject nuevoItem = Instantiate(prefabItemMenu, contenedor.transform);

            CargarPlato ui = nuevoItem.GetComponent<CargarPlato>();

            ui.RellenarInfoPlato(plato, this);
        }
    }

    void CargarMenu(List<Plato> listaAMostrar, Transform contenedor, GameObject prefabItemMenu)
    {
        Debug.Log("Cargar menu");

        foreach (Transform hijo in contenedor) Destroy(hijo.gameObject);

        ToggleGroup contenedorGrupo = contenedor.GetComponent<ToggleGroup>();

        foreach (Plato plato in listaAMostrar)
        {
            GameObject nuevoItem = Instantiate(prefabItemMenu, contenedor.transform);

            Toggle toggleDelPlato = nuevoItem.GetComponentInChildren<Toggle>();
            if (toggleDelPlato != null && contenedorGrupo != null)
            {
                toggleDelPlato.group = contenedorGrupo;
            }

            CargarPlato ui = nuevoItem.GetComponent<CargarPlato>();
            ui.RellenarInfoPlato(plato, this);
        }
    }

    public Plato encontrarPlato(string nombrePlato)
    {
        return todosLosPlatos.FirstOrDefault(p => p.nombre == nombrePlato);
    }
    public void SeleccionarPlato(Plato plato)
    {
        panelMenu.SetActive(false);
        panelInfoPlato.SetActive(true);
        controladorInfoPlato.MostrarInformacion(plato);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}