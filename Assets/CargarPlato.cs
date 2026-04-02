using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CargarPlato : MonoBehaviour
{
    public TextMeshProUGUI textoNombre;
    public TextMeshProUGUI textoPrecio;
    public Image imagenPlato;

    private Plato plato;
    private ControladorMenu controladorMenu;

    public void RellenarInfoPlato(Plato plato, ControladorMenu controladorMenu)
    {
        this.plato = plato;
        this.controladorMenu = controladorMenu;

        // "Pintamos" la información en el botón
        if (textoNombre != null) textoNombre.text = plato.nombre;
        if (textoPrecio != null) textoPrecio.text = plato.precio.ToString("F2") + "€";
        if (imagenPlato != null) imagenPlato.sprite = plato.imagenPlato;

        // 3. Opcional: Si el botón tiene un componente Button, añadimos el clic
        GetComponent<Button>().onClick.AddListener(AlPulsarBotón);
    }

    void AlPulsarBotón()
    {
        // Avisamos al controlador de que este plato ha sido seleccionado
        controladorMenu.SeleccionarPlato(this.plato);
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}