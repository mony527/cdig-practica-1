using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static ControladorPedidos;

public class ControladorBienvenida : MonoBehaviour
{
    public GameObject panelBienvenida;
    public GameObject panelMenu;
    public TextMeshProUGUI textNumComensales;
    private int numComensales;
    public Slider sliderComensales;

    public static ControladorBienvenida instancia { get; private set; }

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
        panelBienvenida.SetActive(true);
        numComensales = 1;
    }

    public void IrAmenu()
    {
        panelBienvenida.SetActive(false);
        panelMenu.SetActive(true);
        ControladorPedidos.instancia.GuardarComensales(numComensales);
    }

    public void ElegirComensales()
    {
        numComensales = (int)sliderComensales.value;
        textNumComensales.text = "" + numComensales;
    }

    public void Reiniciar(){
        numComensales = 1;
        sliderComensales.value = 1;
    }

}