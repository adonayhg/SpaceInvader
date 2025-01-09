using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float speed = 20f; // Velocidad normal del jugador
    public GameObject proyectilPrefab;
    public GameObject proyectilEspecialPrefab;
    public Transform puntoDisparo;
    public Transform puntoDisparo2;
    public Transform puntoDisparo3;

    [Header("Disparo")]
    public float tiempoEntreDisparos = 0.5f; // Tiempo entre disparos
    private float tiempoProximoDisparo = 0f; // Temporizador para el disparo

    public float tiempoEntreDisparosEsp = 5f; // Tiempo entre disparos
    private float tiempoProximoDisparoEsp = 0f; // Temporizador para el disparo

    [Header("Power-Ups")]
    public bool invertirControles = false; // Bandera para invertir los controles

    void Update()
    {
        // Movimiento del jugador
        float horizontal = Input.GetAxis("Horizontal");
        if (invertirControles)
        {
            horizontal = -horizontal; // Invertir los controles si está activo
        }

        // Calcular la dirección del movimiento
        Vector3 direction = new Vector3(horizontal, 0, 0);

        // Comprobar si hay una pared en la dirección del movimiento
        if (horizontal != 0)
        {
            float direccionMovimiento = Mathf.Sign(horizontal); // 1 para derecha, -1 para izquierda
            if (!IsAtWall(transform.position, direccionMovimiento))
            {
                // Mover al jugador solo si no hay una pared
                transform.Translate(direction * speed * Time.deltaTime, Space.World);
            }
        }

        // Disparo de proyectiles
        if ((Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1")) && Time.time >= tiempoProximoDisparo)
        {
            Disparar();
            tiempoProximoDisparo = Time.time + tiempoEntreDisparos; // Actualizar temporizador para el próximo disparo
        }

        if((Input.GetKey(KeyCode.V) || Input.GetButton("Fire2")) && Time.time >= tiempoProximoDisparoEsp)
        {
            DisparoEspecial();
            tiempoProximoDisparoEsp = Time.time + tiempoEntreDisparosEsp; // Actualizar temporizador para el próximo disparo
        }
    }

    void Disparar()
    {
        Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);
        Instantiate(proyectilPrefab, puntoDisparo2.position, puntoDisparo2.rotation);
    }
    void DisparoEspecial()
    {
        Instantiate(proyectilEspecialPrefab, puntoDisparo3.position, puntoDisparo3.rotation);
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
        invertirControles = true;
        Invoke(nameof(DesactivarInvertirControles), duracion);
    }

    void DesactivarInvertirControles()
    {
        invertirControles = false;
    }

    public void ActivarDisparoMasRapido(float duracion)
    {
        tiempoEntreDisparos /= 2; // Reducir a la mitad el tiempo entre disparos
        Invoke(nameof(DesactivarDisparoMasRapido), duracion);
    }

    void DesactivarDisparoMasRapido()
    {
        tiempoEntreDisparos *= 2; // Restaurar el tiempo normal entre disparos
    }

    public void ActivarVelocidadAumentada(float duracion)
    {
        speed *= 2; // Duplica la velocidad
        Invoke(nameof(DesactivarVelocidadAumentada), duracion);
    }

    void DesactivarVelocidadAumentada()
    {
        speed /= 2; // Restaurar la velocidad normal
    }

    public LayerMask wallLayer; // LayerMask para las paredes

    bool IsAtWall(Vector3 posicion, float direccion)
    {
        // Realizar un raycast para verificar si hay una pared delante
        Vector3 direccionRayo = direccion > 0 ? Vector3.right : Vector3.left;
        RaycastHit hit;
        if (Physics.Raycast(posicion, direccionRayo, out hit, 1f, wallLayer))
        {
            return true; // Pared detectada
        }
        return false;
    }
}