using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicaBotonSiguiente : MonoBehaviour
{
    public Button botonSiguiente;
    public Transform contenedor;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ValidarBoton()
    {
        botonSiguiente.interactable = contenedor.GetComponent<ToggleGroup>().AnyTogglesOn();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
