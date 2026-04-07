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
    public TextMeshProUGUI textoEstadoGeneral; //mirar estos dos si van así 
    public Button botonTerminarRonda;

    private bool rondaListaParaTerminar = false;
    private Dictionary<int, List<Plato>> pedidos;
    private List<ControladorComensalRonda> comensalesUI = new List<ControladorComensalRonda>();
    private int rondaActual = 0;
    private int numeroComensales = 0;
    private readonly string[] nombresRonda = {
        "PRIMEROS",
        "SEGUNDOS",
        "POSTRES",
        "BEBIDAS",
        "CAFÉ"
    };

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
        Debug.Log("ControladorRonda arrancando");
       
        
    }

    public void IniciarRondas(Dictionary<int, List<Plato>> pedidosRecibidos) //cambiar el nombre de pedidosRecibidos
    {
        pedidos = pedidosRecibidos;
        rondaActual = 0;

        /*if (pedidos == null || !pedidos.ContainsKey(0))
        {
            Debug.LogWarning("No hay pedidos para iniciar las rondas.");
            return;
        }*/

        numeroComensales = pedidos[0].Count;
        MostrarRondaActual();
    }

    private void MostrarRondaActual()
    {
        if (rondaActual >= nombresRonda.Length)
        {
            IrAPantallaFinal();
            return;
        }

        textoTituloRonda.text = nombresRonda[rondaActual];

        CrearComensales(numeroComensales);
        CargarPlatosDeLaRonda(rondaActual);

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
                Debug.LogError("El prefab no tiene UIComensalRonda.");
            }
        }
    }

    private void CargarPlatosDeLaRonda(int categoria)
    {
        List<Plato> platosRonda = null;

        if (pedidos.ContainsKey(categoria))
        {
            platosRonda = pedidos[categoria];
        }

        for (int i = 0; i < comensalesUI.Count; i++)
        {
            Plato plato = null;

            if (platosRonda != null && i < platosRonda.Count)
            {
                plato = platosRonda[i];
            }

            comensalesUI[i].Configurar(i + 1, plato);
        }

        textoEstadoGeneral.text = "Estado: pendiente";
    }

    private IEnumerator SimularEstadoGeneral()
    {
        textoEstadoGeneral.text = "Estado: pendiente";
        yield return new WaitForSeconds(2f);

        textoEstadoGeneral.text = "Estado: en preparación";
        yield return new WaitForSeconds(4f);

        textoEstadoGeneral.text = "Estado: listo para servir";
        yield return new WaitForSeconds(2f);

        textoEstadoGeneral.text = "Estado: entregado";

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
            panelPago.SetActive(true);
        else
            Debug.Log("Se han terminado todas las rondas.");
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
