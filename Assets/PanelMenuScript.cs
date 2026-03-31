using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMenuScript : MonoBehaviour
{
    public GameObject panelMenu;
    public GameObject panelElegirPlatoComensal;

    // Start is called before the first frame update
    void Start()
    {
        panelMenu.SetActive(false);
    }

    public void HacerPedido()
    {
        panelMenu.SetActive(false);
        panelElegirPlatoComensal.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
