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
    public int numComensales;

    // Start is called before the first frame update
    void Start()
    {
        panelBienvenida.SetActive(true);
    }

    public void IrAmenu()
    {
        panelBienvenida.SetActive(false);
        panelMenu.SetActive(true);
    }

    public void AniadirComensal()
    {
        if (numComensales<3) numComensales++;
        //textNumComensales. numComensales);
    }

    public void QuitarComensal()
    {
        if (numComensales > 1) numComensales--;
        //textNumComensales = new TextMeshProUGUI(numComensales);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
