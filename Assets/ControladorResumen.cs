using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControladorResumen : MonoBehaviour
{
    public GameObject contenedor;
    public GameObject prefab;
    public GameObject panelResumenPedido;

    // Start is called before the first frame update
    void Start()
    {
        panelResumenPedido.SetActive(false);
        
    }

    void CargarResumenPedido()
    {
        Dictionary<int, List<Plato>> platos = ControladorPedidos.instancia.pedidos;

        for (int comensal = 0; comensal<platos[0].Count; comensal++)
        {

            GameObject imgPlato = Instantiate(prefab, contenedor.transform);

            //CargarPlato cargarPlato = nuevoItem.GetComponent<CargarPlato>();

            //cargarPlato.RellenarInfoPlato(plato, this);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
