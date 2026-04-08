using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControladorCuentaUI : MonoBehaviour
{
    public static ControladorCuentaUI instancia {  get;  set; }

    public GameObject panelCuenta;
    public GameObject panelBienvenida;

    public GameObject panelPagarEfectivo;
    public GameObject panelPagarTarjeta;
    public Button botonEfectivo;
    public Button botonTarjeta;

    List<TextMeshProUGUI> totales;

    public TextMeshProUGUI totalPrimeros;
    public TextMeshProUGUI totalSegundos;
    public TextMeshProUGUI totalPostres;
    public TextMeshProUGUI totalBebidas;
    public TextMeshProUGUI totalCafes;

    public TextMeshProUGUI textTotal;
    public TextMeshProUGUI totalPanelEfectivo;
    public TextMeshProUGUI totalPanelTarjeta;

    private float total;

    public float tiempoTotal = 5f;
    private float tiempoTranscurrido = 0f;
    private bool estaCargando = false;
    
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
        panelPagarEfectivo.SetActive(false);
        panelPagarTarjeta.SetActive(false);
        totales = new List<TextMeshProUGUI>();
        totales.Add(totalPrimeros);
        totales.Add(totalSegundos);
        totales.Add(totalPostres);
        totales.Add(totalBebidas);
        totales.Add(totalCafes);


    }
    
    public void CargarTotales(Dictionary<int, List<Plato>> platosTotales)
    {
        total = 0;
        float totalPlato = 0;
        int numeroPlato = 0;
        foreach (TextMeshProUGUI textTotal in totales)
        {
            List<Plato> platosCategoria = platosTotales[numeroPlato];
            totalPlato = CalcularTotal(platosCategoria);
            textTotal.text = totalPlato.ToString("F2") + "€";
            total += totalPlato;
            numeroPlato++;
        }

        this.textTotal.text = total.ToString("F2") + "€";
        



    }
    float CalcularTotal(List<Plato> platosCategoria)
    {
        float totalCategoria = 0;

        foreach (Plato plato in platosCategoria)
        {
            if (plato == null)
            {
                totalCategoria += 0;

            }
            else
            {
                totalCategoria += plato.precio;
            }
                
        }

        return totalCategoria;

    }
    public void PagarEfectivo()
    {
        tiempoTranscurrido = 0f;
        panelPagarEfectivo.GetComponentInChildren<Slider>().value = 0;
        estaCargando = true;

        panelPagarEfectivo.SetActive(true);

        totalPanelEfectivo.text = this.total.ToString("F2") + "€";

        botonEfectivo.interactable = false;
        botonTarjeta.interactable = false;
    }
    public void PagarTarjeta()
    {
        tiempoTranscurrido = 0f;
        panelPagarTarjeta.GetComponentInChildren<Slider>().value = 0;
        estaCargando = true;

        totalPanelTarjeta.text = this.total.ToString("F2") + "€";

        panelPagarTarjeta.SetActive(true);
        botonEfectivo.interactable = false;
        botonTarjeta.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (estaCargando)
        {
            tiempoTranscurrido += Time.deltaTime;
            panelPagarEfectivo.GetComponentInChildren<Slider>().value = tiempoTranscurrido / tiempoTotal;
            panelPagarTarjeta.GetComponentInChildren<Slider>().value = tiempoTranscurrido / tiempoTotal;

            if (tiempoTranscurrido >= tiempoTotal)
            {
                estaCargando = false;
                panelCuenta.SetActive(false);
                panelBienvenida.SetActive(true);
                panelPagarEfectivo.SetActive(false);
                panelPagarTarjeta.SetActive(false);
                
            }
        }
    }
}
