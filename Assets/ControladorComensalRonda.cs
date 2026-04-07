using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControladorComensalRonda : MonoBehaviour
{
    public TMP_Text textoComensal;
    public Image imagenPlato;

    public void Configurar(int numeroComensal, Plato plato)
    {
        textoComensal.text = "Comensal " + numeroComensal;

        if (plato != null)
        {
            imagenPlato.sprite = plato.imagenPlato;
            imagenPlato.gameObject.SetActive(true);
        }
        else
        {
            imagenPlato.gameObject.SetActive(false);
        }
    }
    /*// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
