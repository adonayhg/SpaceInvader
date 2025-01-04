using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigosPuntos : MonoBehaviour
{
    public int puntos = 10; // Puntos que otorga este enemigo al ser destruido
    private PowerUpManager powerUpManager;

    void Start()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ProyectilJugador")) // Detecta colisión con proyectil del jugador
        {
            // Sumar puntos al jugador
            SistemaDeJuego.instancia.AgregarPuntos(puntos);

            // Intentar soltar un power-up
            if (powerUpManager != null)
            {
                powerUpManager.IntentarSoltarPowerUp(transform.position);
            }

            // Destruir al enemigo y al proyectil
            Destroy(other.gameObject); // Destruye el proyectil
            Destroy(gameObject);       // Destruye al enemigo
        }
    }
}