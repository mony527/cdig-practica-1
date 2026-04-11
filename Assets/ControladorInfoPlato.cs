using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControladorInfoPlato : MonoBehaviour
{
    public TextMeshProUGUI txtNombre;
    public TextMeshProUGUI txtIngredientes;
    public TextMeshProUGUI txtPrecio;
    public Image imgPlato;
    public GameObject panelInfo;
    public GameObject panelMenu;

    
    void Start()
    {
        panelInfo.SetActive(false);
    }

    
    public void MostrarInformacion(Plato plato)
    {
        
        txtNombre.text = plato.nombre;
        txtIngredientes.text = plato.ingredientes;
        txtPrecio.text = "<b>Precio:</b> " + plato.precio.ToString("F2") + "�";
        imgPlato.sprite = plato.imagenPlato;

        
        panelMenu.SetActive(false);
        panelInfo.SetActive(true);
    }

    
    public void CerrarPanel()
    {
        panelInfo.SetActive(false);
        panelMenu.SetActive(true);
    }

}
