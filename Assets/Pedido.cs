[System.Serializable]
public class SeleccionComensal
{
    public int idComensal;
    public Plato primero;
    public Plato segundo;
    public Plato postre;
    public Plato bebida;
    public Plato cafe;
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pedido : MonoBehaviour
{
    private Plato primero;
    private Plato segundo;
    private Plato postre;
    private Plato bebida;
    private Plato cafe;

    public Pedido(){}

    public void setPrimero(string nombre, Image imagenPlato, string ingredientes, float precio)
    {
        this.primero = new Plato(nombre, imagenPlato, ingredientes, precio, TipoPlato.Primero);
    }

    public void setSegundo(string nombre, Image imagenPlato, string ingredientes, float precio)
    {
        this.primero = new Plato(nombre, imagenPlato, ingredientes, precio, TipoPlato.Segundo);
    }

    public void setPostre(string nombre, Image imagenPlato, string ingredientes, float precio)
    {
        this.primero = new Plato(nombre, imagenPlato, ingredientes, precio, TipoPlato.Postre);
    }

    public void setBebida(string nombre, Image imagenPlato, string ingredientes, float precio)
    {
        this.primero = new Plato(nombre, imagenPlato, ingredientes, precio, TipoPlato.Bebida);
    }

    public void setCafe(string nombre, Image imagenPlato, string ingredientes, float precio)S
    {
        this.primero = new Plato(nombre, imagenPlato, ingredientes, precio, TipoPlato.Cafe);
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
*/
