using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float speed = 20f;
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public Transform puntoDisparo2;

    void Update()
    {
        // Movimiento del jugador
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontal, 0, 0);
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Disparo de proyectiles
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Disparar();
        }
    }

    void Disparar()
    {
        Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);
        Instantiate(proyectilPrefab, puntoDisparo2.position, puntoDisparo2.rotation);
    }

    // Detectar colisiones con proyectiles enemigos
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ProyectilEnemigo"))
        {
            // Reducir una vida al jugador
            SistemaDeJuego.instancia.PerderVida();

            // Destruir el proyectil enemigo
            Destroy(other.gameObject);
        }
    }
}