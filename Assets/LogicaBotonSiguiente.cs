using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicaBotonSiguiente : MonoBehaviour
{
    public Button botonSiguiente;
    public Transform contenedor;
    
    public void ValidarBoton()
    {
        botonSiguiente.interactable = contenedor.GetComponent<ToggleGroup>().AnyTogglesOn();
    }
    
    
}
