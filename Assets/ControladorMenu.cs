using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Para filtrar fácilmente


public class ControladorMenu : MonoBehaviour
{
    public GameObject panelMenu;
    public GameObject panelElegirPlatoComensal;

    public List<Plato>todosLosPlatos; // Arrastra aquí todos tus archivos .asset
    public GameObject prefabItemMenu; // Tu prefab 'ButtonPlano'
    public Transform contenedor; // El objeto con el 'Grid Layout Group'

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
    public void FiltrarPorCategoria(string categoriaString)
    {
        // Convertimos el texto del botón a el Enum TipoPlato
        TipoPlato cat = (TipoPlato)System.Enum.Parse(typeof(TipoPlato), categoriaString);

        // Filtramos la lista usando LINQ
        var filtrados = todosLosPlatos.Where(p => p.tipo == cat).ToList();
        CargarMenu(filtrados);
    }

    void CargarMenu(List<Plato> listaAMostrar)
    {
        // 1. Limpiar el contenedor
        foreach (Transform hijo in contenedor) Destroy(hijo.gameObject);

        // 2. Crear los platos
        foreach (Plato plato in listaAMostrar)
        {
            GameObject nuevoItem = Instantiate(prefabItemMenu, contenedor);

            // Suponiendo que tu prefab tiene un script 'ItemMenuUI' para ponerse sus datos
            CargarPlato ui = nuevoItem.GetComponent<CargarPlato>();
            Debug.Log($"Contenedor: {contenedor}, Prefab: {prefabItemMenu}, Lista: {listaAMostrar}");

            ui.RellenarInfoPlato(plato, this);
        }
    }

    public void SeleccionarPlato(Plato datos)
    {
        // Llamamos al panel de detalle para que se rellene y se muestre
        //scriptDetalle.AbrirPanel(datos);
        // Opcional: Desactivamos el Panel del Menú
        // gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}