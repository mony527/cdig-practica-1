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

        
        if (textoNombre != null) textoNombre.text = plato.nombre;
        if (textoPrecio != null) textoPrecio.text = plato.precio.ToString("F2") + "€";
        if (imagenPlato != null) imagenPlato.sprite = plato.imagenPlato;

        Button boton = GetComponent<Button>();
        if (boton != null)
        {
            boton.onClick.RemoveAllListeners();
            boton.onClick.AddListener(AlPulsarImagen);
        }
    }

    void AlPulsarImagen()
    {
        Debug.Log("¡He detectado el clic en el plato: " + this.plato.nombre + "!");
        ControladorInfoPlato info = FindObjectOfType<ControladorInfoPlato>();


        if (info != null)
        {
            info.MostrarInformacion(this.plato);
        }
        else
        {
            Debug.LogError("¡No encuentro el PanelInfoPlato en la escena!");
        }
    }
}
