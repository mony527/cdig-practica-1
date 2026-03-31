using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelBienvenidaScript : MonoBehaviour
{
    public GameObject panelBienvenida;
    public GameObject panelMenu;
    public TextMeshProUGUI textNumComensales;
    public int numComensales = 1;
    public Button btnAniadir;
    public Button btnQuitar;

    // Start is called before the first frame update
    void Start()
    {
        panelBienvenida.SetActive(true);
    }

    public void IrAmenu()
    {
        panelBienvenida.SetActive(false);
        panelMenu.SetActive(true);
        //Pasar numComensales al Controlador de Pedidos
    }

    public void AniadirComensal()
    {
        if (numComensales < 4)
        {
            btnAniadir.interactable = true;
            btnQuitar.interactable = true;
            numComensales++;
            textNumComensales.text = "" + numComensales;
            if(numComensales == 4) btnAniadir.interactable = false;

        }
        else
        {
            btnAniadir.interactable = false;
        }

    }

    public void QuitarComensal()
    {
        if (numComensales > 1)
        {
            btnQuitar.interactable = true;
            btnAniadir.interactable = true;
            numComensales--;
            textNumComensales.text = "" + numComensales;
            if (numComensales == 1) btnQuitar.interactable = false;
        }
        else
        {
            btnQuitar.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
