using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilEnemigo : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad del proyectil

    void Update()
    {
        // Mover el proyectil hacia abajo
        transform.Translate(new Vector3(0,0,-1) * velocidad * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Si el proyectil colide con algo, destrúyelo
        Destroy(gameObject);
    }
}
