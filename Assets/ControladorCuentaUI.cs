using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorCuentaUI : MonoBehaviour
{
    public GameObject panelPagarEfectivo;
    public GameObject panelPagarTarjeta;
    public Button botonEfectivo;
    public Button botonTarjeta;
    // Start is called before the first frame update
    void Start()
    {
        panelPagarEfectivo.SetActive(false);
        panelPagarTarjeta.SetActive(false);
        
    }
    

    public void PagarEfectivo()
    {
        panelPagarEfectivo.SetActive(true);
        botonEfectivo.interactable = false;
        botonTarjeta.interactable = false;
    }
    public void PagarTarjeta()
    {
        panelPagarTarjeta.SetActive(true);
        botonEfectivo.interactable = false;
        botonTarjeta.interactable = false;
    }

    public void CancelarPago()
    {
        panelPagarEfectivo.SetActive(false);
        panelPagarTarjeta.SetActive(false);
        botonEfectivo.interactable = true;
        botonTarjeta.interactable = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
