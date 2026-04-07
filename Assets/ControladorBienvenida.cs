using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static ControladorPedidos;

public class PanelBienvenidaScript : MonoBehaviour
{
    public GameObject panelBienvenida;
    public GameObject panelMenu;
    public TextMeshProUGUI textNumComensales;
    private int numComensales;
    public Button btnAniadir;
    public Button btnQuitar;
    public TextMeshProUGUI textAviso;

    public static PanelBienvenidaScript instancia { get; private set; }

    private void Awake()
    {
        // Primero configuramos el Singleton
        if (instancia != null && instancia != this)
        {
            Destroy(this.gameObject);
            return; // Salimos para que no se ejecute nada más en este objeto "duplicado"
        }
        instancia = this;
        //DontDestroyOnLoad(this.gameObject);
    }

    public int  NumComensales => numComensales; // Qué es esto??

    // Start is called before the first frame update
    void Start()
    {
        panelBienvenida.SetActive(true);
        textAviso.enabled = false;
        numComensales = 1;
    }

    public void IrAmenu()
    {
        panelBienvenida.SetActive(false);
        panelMenu.SetActive(true);
        ControladorPedidos.instancia.GuardarComensales(numComensales);
    }

    public void AniadirComensal()
    {
        if (numComensales < 4)
        {
            textAviso.enabled = false;
            btnAniadir.interactable = true;
            btnQuitar.interactable = true;
            numComensales++;
            textNumComensales.text = "" + numComensales;
            if (numComensales == 4)
            {
                btnAniadir.interactable = false;
                textAviso.enabled = true;
            }

        }
        else
        {
            btnAniadir.interactable = false;
            textAviso.enabled = true;
        }

    }

    public void QuitarComensal()
    {
        if (numComensales > 1)
        {
            textAviso.enabled = false;
            btnQuitar.interactable = true;
            btnAniadir.interactable = true;
            numComensales--;
            textNumComensales.text = "" + numComensales;
            if (numComensales == 1) { 
                btnQuitar.interactable = false;
                textAviso.enabled = true;
            }
        }
        else
        {
            btnQuitar.interactable = false;
            textAviso.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
