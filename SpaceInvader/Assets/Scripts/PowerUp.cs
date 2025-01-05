using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum TipoPowerUp
    {
        RepararBunkers,
        InvertirControles,
        DispararMasRapido,
        MoverseMasRapido
    }
    public float velocidad = 5f;
    void Update()
    {
        // Mover el proyectil hacia abajo
        transform.Translate(new Vector3(0, 0, -1) * velocidad * Time.deltaTime);
    }


    public TipoPowerUp tipoPowerUp;
    public float duracion = 10f; // Duraci�n de los efectos temporales (como invertir controles o moverse m�s r�pido)

    void OnTriggerEnter(Collider other)
    {
        // Detectar colisi�n con el jugador
        if (other.CompareTag("Jugador"))
        {
            ActivarEfecto(other.gameObject); // Pasar el GameObject del jugador
            Destroy(gameObject); // Destruir el power-up despu�s de activarlo
        }
    }


    void ActivarEfecto(GameObject jugador)
    {
        // Obtener el script del jugador para aplicar los efectos
        Jugador scriptJugador = jugador.GetComponent<Jugador>();
        if (scriptJugador == null)
        {
            Debug.LogWarning("No se encontr� el script Jugador en el objeto colisionado.");
            return;
        }

        switch (tipoPowerUp)
        {
            case TipoPowerUp.RepararBunkers:
                RepararBunkers();
                break;
            case TipoPowerUp.InvertirControles:
                scriptJugador.ActivarInvertirControles(duracion);
                break;
            case TipoPowerUp.DispararMasRapido:
                scriptJugador.ActivarDisparoMasRapido(duracion);
                break;
            case TipoPowerUp.MoverseMasRapido:
                scriptJugador.ActivarVelocidadAumentada(duracion);
                break;
        }
    }

    void RepararBunkers()
    {
        // Encuentra todos los objetos con el script Bunkers en la escena
        Bunkers[] todosLosBunkers = FindObjectsOfType<Bunkers>();

        // Si no hay b�nkers, muestra un mensaje
        if (todosLosBunkers.Length == 0)
        {
            Debug.LogWarning("No se encontraron b�nkers en la escena.");
            return;
        }

        // Repara cada b�nker
        foreach (Bunkers bunker in todosLosBunkers)
        {
            bunker.RecuperarBunkers(); // Llama al m�todo para restaurar los b�nkers
        }

        Debug.Log("�Todos los b�nkers han sido reparados!");

    }

    System.Collections.IEnumerator InvertirControles()
    {
        Jugador jugador = FindObjectOfType<Jugador>();
        if (jugador != null)
        {
            jugador.invertirControles = true;
            Debug.Log("�Controles invertidos!");
            yield return new WaitForSeconds(duracion);
            jugador.invertirControles = false;
            Debug.Log("Los controles han vuelto a la normalidad.");
        }
    }

    System.Collections.IEnumerator DispararMasRapido()
    {
        Jugador jugador = FindObjectOfType<Jugador>();
        if (jugador != null)
        {
            jugador.tiempoEntreDisparos /= 2; // Dispara el doble de r�pido
            Debug.Log("�Disparo m�s r�pido activado!");
            yield return new WaitForSeconds(duracion);
            jugador.tiempoEntreDisparos *= 2; // Regresa al tiempo normal
            Debug.Log("Disparo m�s r�pido desactivado.");
        }
    }

    System.Collections.IEnumerator MoverseMasRapido()
    {
        Jugador jugador = FindObjectOfType<Jugador>();
        if (jugador != null)
        {
            jugador.speed *= 2; // Duplica la velocidad de la nave
            Debug.Log("�Velocidad aumentada!");
            yield return new WaitForSeconds(duracion);
            jugador.speed /= 2; // Regresa a la velocidad normal
            Debug.Log("Velocidad aumentada desactivada.");
        }
    }
}
