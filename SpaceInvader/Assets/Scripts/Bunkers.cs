using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunkers : MonoBehaviour
{
    public int vidas = 5; // Número de vidas del bunker
    public GameObject bunker;

    // Cuando el bunker colisiona con un proyectil enemigo
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ProyectilEnemigo")|| other.CompareTag("ProyectilJugador"))
        {
            vidas--;

            Destroy(other.gameObject);

            if (vidas <= 0)
            {
                DestruirBunker();
            }
        }
    }
    void DestruirBunker()
    {
        bunker.SetActive(false);
    }
}