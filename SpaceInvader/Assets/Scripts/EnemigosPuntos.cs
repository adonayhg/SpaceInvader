using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemigosPuntos : MonoBehaviour
{
    public int puntos = 10; // Puntos que otorga este enemigo al ser destruido
    private PowerUpManager powerUpManager;
    void Start()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
        projectilEspecial = FindObjectOfType<ProjectilEspecial>();
        enemigos = FindObjectOfType<Enemigos>();
    }

    [SerializeField] private Enemigos enemigos;
    [SerializeField] private ProjectilEspecial projectilEspecial;
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



            Destroy(other.gameObject);// Destruye el proyectil
            if (enemigos != null)
            {
                enemigos.EnemigoDestruido(gameObject); // Se notifica la destrucción
                enemigos.RestarEnemigo(); // Llamamos a RestarEnemigo para actualizar el contador
            }

            Destroy(gameObject);       // Destruye al enemigo
        }

        if (other.CompareTag("ProyectilEspecial"))
        {

            // Sumar puntos al jugador
            SistemaDeJuego.instancia.AgregarPuntos(puntos);

            // Intentar soltar un power-up
            if (powerUpManager != null)
            {
                powerUpManager.IntentarSoltarPowerUp(transform.position);
            }

            if (enemigos != null)
            {
                enemigos.EnemigoDestruido(gameObject); // Se notifica la destrucción
                enemigos.RestarEnemigo(); // Llamamos a RestarEnemigo para actualizar el contador
            }

            Destroy(gameObject);       // Destruye al enemigo
            Destroy(other.gameObject);// Destruye el proyectil

            ProjectilEspecial.instancia.RadioProyectil();

        }
        if (other.CompareTag("Explosion"))
        {

            // Sumar puntos al jugador
            SistemaDeJuego.instancia.AgregarPuntos(puntos);

            // Intentar soltar un power-up
            if (powerUpManager != null)
            {
                powerUpManager.IntentarSoltarPowerUp(transform.position);
            }

            if (enemigos != null)
            {
                enemigos.EnemigoDestruido(gameObject); // Se notifica la destrucción
                enemigos.RestarEnemigo(); // Llamamos a RestarEnemigo para actualizar el contador
            }

            Destroy(gameObject);       // Destruye al enemigo
            Destroy(other.gameObject);// Destruye el proyectil            nobala=false;
        }

    }
}