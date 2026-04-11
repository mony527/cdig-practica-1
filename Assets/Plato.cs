using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum TipoPlato
{
    Primero,
    Segundo,
    Postre,
    Bebida,
    Café
}

[CreateAssetMenu(fileName = "NuevoPlato", menuName = "Restaurante/Plato")]
public class Plato : ScriptableObject
{
    public string nombre;
    public TipoPlato tipo;
    [TextArea] public string ingredientes;
    public float precio;
    public Sprite imagenPlato;

    

    public Plato(string nombre, Sprite imagenPlato, string ingredientes, float precio, TipoPlato tipo)
    {
        this.nombre = nombre;
        this.imagenPlato = imagenPlato;
        this.ingredientes = ingredientes;
        this.precio = precio;
        this.tipo = tipo;
    }
}
