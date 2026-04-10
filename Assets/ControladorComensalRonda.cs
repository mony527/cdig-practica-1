using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControladorComensalRonda : MonoBehaviour
{
    public TMP_Text textoComensal;
    public Image imagenPlato;

    public void Configurar(int numeroComensal, Plato plato, Sprite imagenXSinPlato)
    {
        textoComensal.text = "Comensal " + numeroComensal;

        if (plato != null)
        {
            imagenPlato.sprite = plato.imagenPlato;
            imagenPlato.gameObject.SetActive(true);
        }
        else
        {
            imagenPlato.sprite = imagenXSinPlato;
            imagenPlato.gameObject.SetActive(true);
        }
    }

}
