using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControladorRonda : MonoBehaviour
{
    public static ControladorRonda instancia { get; private set; }
    public Transform contenedorComensales;
    public GameObject prefabComensal;
    public GameObject panelRonda;
    public GameObject panelPago;
    public TextMeshProUGUI textoTituloRonda;
    public TextMeshProUGUI textoEstado;
    public Button botonTerminarRonda;
    public Sprite imagenXSinPlato;

    private bool rondaListaParaTerminar = false;
    private Dictionary<int, List<Plato>> pedidos;
    private List<ControladorComensalRonda> comensalesUI = new List<ControladorComensalRonda>();
    private int rondaActual = 0;
    private int numeroComensales = 0;
    private readonly string[] nombresRonda = {
        "PRIMEROS",
        "SEGUNDOS",
        "POSTRES",
        "CAFÉ"
    };

    private readonly int[] categoriasRonda = {
        0, // PRIMEROS
        1, // SEGUNDOS
        2, // POSTRES
        4  // CAFÉ
    };

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
        panelRonda.SetActive(false);
        Debug.Log("ControladorRonda arrancando");
    }

    public void IniciarRondas(Dictionary<int, List<Plato>> pedidosRecibidos) 
    {
        pedidos = pedidosRecibidos;
        rondaActual = 0;


        numeroComensales = pedidos[0].Count;
        MostrarRondaActual();
    }

    private void MostrarRondaActual()
    {
        List<Plato> platosRonda = null;
        
        int categoriaReal = categoriasRonda[rondaActual];

        if (pedidos.ContainsKey(categoriaReal))
        {
            platosRonda = pedidos[categoriaReal];
        }
        
        int i = 0;
        bool todosVacios = true;

        Debug.Log("Ronda: " + rondaActual);
        while (i < platosRonda.Count && todosVacios){
            Debug.Log("hola");
            if (platosRonda[i] != null)
            {
                todosVacios = false;
            }
            i++;
        }
  
        if (rondaActual >= nombresRonda.Length || todosVacios)
        {
            IrAPantallaFinal();
            return;
        }

        textoTituloRonda.text = nombresRonda[rondaActual];

        CrearComensales(numeroComensales);
        CargarPlatosDeLaRonda(platosRonda);

        

        rondaListaParaTerminar = false;

        if (botonTerminarRonda != null)
        {
            botonTerminarRonda.interactable = false;
        }

        StopAllCoroutines();
        StartCoroutine(SimularEstadoGeneral());
    }

    private void CrearComensales(int num)
    {
        foreach (Transform child in contenedorComensales)
        {
            Destroy(child.gameObject);
        }

        comensalesUI.Clear();

        for (int i = 0; i < num; i++)
        {
            GameObject obj = Instantiate(prefabComensal, contenedorComensales);
            ControladorComensalRonda ui = obj.GetComponent<ControladorComensalRonda>();

            if (ui != null)
            {
                comensalesUI.Add(ui);
            }
            else
            {
                Debug.LogError("El prefab no tiene ComensalRondaUI.");
            }
        }
    }

    private void CargarPlatosDeLaRonda(List<Plato> platosRonda)
    {

        

        for (int i = 0; i < comensalesUI.Count; i++)
        {
            Plato plato = null;

            if (platosRonda != null && i < platosRonda.Count)
            {
                plato = platosRonda[i];
            }

            comensalesUI[i].Configurar(i + 1, plato, imagenXSinPlato);
        }

        

        textoEstado.text = "Estado: pendiente";
    }

    private IEnumerator SimularEstadoGeneral()
    {
        textoEstado.text = "Estado: pendiente";
        yield return new WaitForSeconds(1f);

        textoEstado.text = "Estado: en preparación";
        yield return new WaitForSeconds(1f);

        textoEstado.text = "Estado: listo para servir";
        yield return new WaitForSeconds(1f);

        textoEstado.text = "Estado: entregado";

        rondaListaParaTerminar = true;

        if (botonTerminarRonda != null)
        {
            botonTerminarRonda.interactable = true;
        }
    }

    public void TerminarRonda()
    {
        if (!rondaListaParaTerminar)
        {
            Debug.Log("La ronda todavía no está entregada.");
            return;
        }

        rondaActual++;

        if (rondaActual < nombresRonda.Length)
        {
            MostrarRondaActual();
        }
        else
        {
            IrAPantallaFinal();
        }
    }

    private void IrAPantallaFinal()
    {
        panelRonda.SetActive(false);

        if (panelPago != null)
        {
            panelPago.SetActive(true);
            ControladorCuentaUI.instancia.CargarTotales(pedidos);

        }
        else
            Debug.Log("Se han terminado todas las rondas.");
    }

}
