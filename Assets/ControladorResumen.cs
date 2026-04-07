using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControladorResumen : MonoBehaviour
{
    public GameObject contenedor;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        panelResumenPedido.SetActive(false);
        
    }

    void CargarResumenPedido()
    {
        Dictionary<int, List<Plato>> platos = ControladorPedido.instancia.pedidos;

        foreach (int comensal in platos[0].length)
        {

            GameObject imgPlato = Instantiate(prefab, contenedor.transform);

            CargarPlato cargarPlato = nuevoItem.GetComponent<CargarPlato>();

            cargarPlato.RellenarInfoPlato(plato, this);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
