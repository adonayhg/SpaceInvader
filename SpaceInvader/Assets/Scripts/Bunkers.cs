using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunkers : MonoBehaviour
{
    public int vidas = 5; // N�mero de vidas del bunker
    public GameObject bunker;

    // Cuando el bunker colisiona con un proyectil enemigo
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ProyectilEnemigo")|| other.CompareTag("ProyectilJugador"))// Detecta colisi�n con proyectil enemigo
        {
            // Reducir las vidas del bunker
            vidas--;

            // Destruir el proyectil enemigo
            Destroy(other.gameObject);

            // Verificar si el bunker debe ser destruido
            if (vidas <= 0)
            {
                DestruirBunker();
            }
        }
    }

    // M�todo para destruir el bunker cuando se quedan sin vidas
    void DestruirBunker()
    {
        bunker.SetActive(false);
    }
}