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

    // Start is called before the first frame update
    void Start()
    {
        panelInfo.SetActive(false);
    }

    // Esta función es la que llama el Menú al pinchar un plato
    public void MostrarInformacion(Plato plato)
    {
        // 1. Rellenamos los datos
        txtNombre.text = plato.nombre;
        txtIngredientes.text = plato.ingredientes;
        txtPrecio.text = "<b>Precio:</b> " + plato.precio.ToString("F2") + "€";
        imgPlato.sprite = plato.imagenPlato;

        // 2. Activamos el panel (si estaba oculto)
        panelMenu.SetActive(false);
        panelInfo.SetActive(true);
    }

    // Para el botón de "Volver"
    public void CerrarPanel()
    {
        panelInfo.SetActive(false);
        panelMenu.SetActive(true);
    }

}
