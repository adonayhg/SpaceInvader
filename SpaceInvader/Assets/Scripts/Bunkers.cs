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
        // Desactivar solo la representación visual y las colisiones
        bunker.GetComponent<MeshRenderer>().enabled = false; // Desactivar el renderizado
        bunker.GetComponent<Collider>().enabled = false;     // Desactivar el collider
    }

    public void RecuperarBunkers()
    {
        vidas = 5;
        bunker.GetComponent<MeshRenderer>().enabled = true;
        bunker.GetComponent<Collider>().enabled = true;
    }
}