using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class ControladorElegirPlatoUI : MonoBehaviour
{
    private ControladorMenu menu;
    public TextMeshProUGUI textoPaso;
    public TextMeshProUGUI tituloPantalla;

    private int numeroComensal;
    private int numeroPlato;
    private const int numeroTotalPlatos = 5;
    private int numTotalComensales;

    private static ControladorPedidos controladorPedidos;

    public GameObject prefabItemMenu;

    public Transform contenedor;
    // Start is called before the first frame update
    void Start()
    {
        numeroPlato = 0;
        numeroComensal = 1;
        controladorPedidos = ControladorPedidos.instancia;
    }

    public void eleccionPlatos()
    {
        switch(numeroPlato)
        {
            case 0:
                numeroComensal = 1;
                numeroPlato = 1;
                Debug.Log(controladorPedidos.numeroComensales);
                numTotalComensales = controladorPedidos.numeroComensales;
                menu = ControladorMenu.instancia;
                Debug.Log(menu.todosLosPlatos.Count);
                tituloPantalla.text = "Elegir el primer plato del comensal " + numeroComensal.ToString();
                textoPaso.text = "Plato " + numeroPlato + " de " + numeroTotalPlatos;
                break;
            case 1:
                if (numeroComensal <= numTotalComensales)
                {
                    tituloPantalla.text = "Elegir el primer plato del comensal " + numeroComensal.ToString();
                    textoPaso.text = "Plato " + numeroPlato + " de " + numeroTotalPlatos;
                    menu.FiltrarPorCategoria("Primeros", contenedor, prefabItemMenu);
                    Debug.Log("antes de guardar");
                    numeroComensal += 1;
                }
                else
                {
                    numeroPlato++;
                    eleccionPlatos();
                }
                break;
        }
        
    }

    public void guardarPlato()
    {

        ToggleGroup contenedorGrupo = contenedor.GetComponent<ToggleGroup>();
        Toggle toggleActivo = contenedorGrupo.GetFirstActiveToggle();

        // Si quieres el objeto "TextNombre" que está al lado del Toggle
        string nombrePlato = toggleActivo.GetComponentInParent<CargarPlato>().textoNombre.text;

        Plato platoComensal = menu.encontrarPlato(nombrePlato);

        controladorPedidos.primerosplatos.Add(platoComensal);

        Debug.Log("Plato guardado: " + platoComensal.name.ToString());

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
