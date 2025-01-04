using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float speed = 20f; // Velocidad normal del jugador
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public Transform puntoDisparo2;

    [Header("Disparo")]
    public float tiempoEntreDisparos = 0.5f; // Tiempo entre disparos
    private float tiempoProximoDisparo = 0f; // Temporizador para el disparo

    [Header("Power-Ups")]
    public bool invertirControles = false; // Bandera para invertir los controles

    void Update()
    {
        // Movimiento del jugador
        float horizontal = Input.GetAxis("Horizontal");
        if (invertirControles)
        {
            horizontal = -horizontal; // Invertir los controles
        }

        Vector3 direction = new Vector3(horizontal, 0, 0);
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Disparo de proyectiles
        if ((Input.GetKey(KeyCode.Space) || Input.GetButton("Fire2")) && Time.time >= tiempoProximoDisparo)
        {
            Disparar();
            tiempoProximoDisparo = Time.time + tiempoEntreDisparos; // Actualizar temporizador para el próximo disparo
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

    // Métodos para manejar los efectos de los power-ups
    public void ActivarInvertirControles(float duracion)
    {
        StartCoroutine(InvertirControles(duracion));
    }

    public void ActivarDisparoMasRapido(float duracion)
    {
        StartCoroutine(DisparoMasRapido(duracion));
    }

    public void ActivarVelocidadAumentada(float duracion)
    {
        StartCoroutine(MoverseMasRapido(duracion));
    }

    // Corrutina para invertir los controles
    private System.Collections.IEnumerator InvertirControles(float duracion)
    {
        invertirControles = true;
        Debug.Log("¡Controles invertidos!");
        yield return new WaitForSeconds(duracion);
        invertirControles = false;
        Debug.Log("Controles restaurados.");
    }

    // Corrutina para disparar más rápido
    private System.Collections.IEnumerator DisparoMasRapido(float duracion)
    {
        tiempoEntreDisparos /= 2; // Reducir el tiempo entre disparos a la mitad
        Debug.Log("¡Disparo más rápido activado!");
        yield return new WaitForSeconds(duracion);
        tiempoEntreDisparos *= 2; // Restaurar el tiempo normal entre disparos
        Debug.Log("Disparo más rápido desactivado.");
    }

    // Corrutina para moverse más rápido
    private System.Collections.IEnumerator MoverseMasRapido(float duracion)
    {
        speed *= 2; // Duplica la velocidad del jugador
        Debug.Log("¡Velocidad aumentada!");
        yield return new WaitForSeconds(duracion);
        speed /= 2; // Restaurar la velocidad normal
        Debug.Log("Velocidad restaurada.");
    }
}