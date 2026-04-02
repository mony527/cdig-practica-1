using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    //public ControladorDetalle scriptDetalle; // Referencia al panel de información extendida

    // Start is called before the first frame update
    void Start()
    {
        panelMenu.SetActive(false);
        MostrarTodos(); // Al empezar, enseñamos todo
    }

    public void HacerPedido()
    {
        panelMenu.SetActive(false);
        panelElegirPlatoComensal.SetActive(true);
    }
    
    // Botón "TODOS"
    public void MostrarTodos() => CargarMenu(todosLosPlatos);

    // Botones de FILTRO (Primeros, Segundos, etc.)
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

    void CargarMenu(List<Plato> listaAMostrar)
    {
        // 1. Limpiar el contenedor
        foreach (Transform hijo in contenedor) Destroy(hijo.gameObject);

        // 2. Crear los platos
        foreach (Plato plato in listaAMostrar)
        {
            GameObject nuevoItem = Instantiate(prefabItemMenu, contenedor.transform);

            // Suponiendo que tu prefab tiene un script 'ItemMenuUI' para ponerse sus datos
            CargarPlato ui = nuevoItem.GetComponent<CargarPlato>();
            Debug.Log($"Contenedor: {contenedor}, Prefab: {prefabItemMenu}, Nombre: {ui.textoNombre.text}, Precio: {ui.textoPrecio.text}");

            ui.RellenarInfoPlato(plato, this);
        }
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