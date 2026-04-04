using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPedidos : MonoBehaviour
{
    public static ControladorPedidos instancia;
    public int numeroComensales;
    public List<Plato> primerosplatos;

    void Awake()
    {
        instancia = this;
    }
    public void GuardarComensales(int comensales)
    {
        numeroComensales = comensales;
        Debug.Log("Número de comensales guardados: " + numeroComensales );

        // Aquí activas la pantalla del Menú y desactivas la de Inicio
        // Y empiezas el bucle: Comensal 1, fase Primero...
    }
}

