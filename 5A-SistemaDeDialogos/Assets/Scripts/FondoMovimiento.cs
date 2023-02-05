using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoMovimiento : MonoBehaviour
{
    public Vector2 velocidadMovimiento; // para el movimiento del fondo.
    private Vector2 offset;
    private Material material; //Referencia al material.
    private Rigidbody2D jugadorRB;//referencia al jugador.

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material; //El material es el mismo que tiene el sprite renderer.
        jugadorRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        offset = (jugadorRB.velocity.x * 0.1f) * velocidadMovimiento * Time.deltaTime;//el offset es igual a la velocidad de time delta time.
        material.mainTextureOffset += offset;
    }
}
