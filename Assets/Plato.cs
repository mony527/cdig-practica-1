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
    Cafe
}

[CreateAssetMenu(fileName = "NuevoPlato", menuName = "Restaurante/Plato")]
public class Plato : ScriptableObject
{
    public string nombre;
    public TipoPlato tipo;
    [TextArea] public string ingredientes;
    public float precio;
    public Sprite imagenPlato;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Plato(string nombre, Sprite imagenPlato, string ingredientes, float precio, TipoPlato tipo)
    {
        this.nombre = nombre;
        this.imagenPlato = imagenPlato;
        this.ingredientes = ingredientes;
        this.precio = precio;
        this.tipo = tipo;
    }
}
