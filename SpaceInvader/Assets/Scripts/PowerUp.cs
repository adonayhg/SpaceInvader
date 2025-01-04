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
        if (other.CompareTag("Jugador")) // Detecta colisi�n con el jugador
        {
            ActivarEfecto();
            Destroy(gameObject); // Destruir el power-up despu�s de recogerlo
        }
    }

    void ActivarEfecto()
    {
        switch (tipoPowerUp)
        {
            case TipoPowerUp.RepararBunkers:
                RepararBunkers();
                break;
            case TipoPowerUp.InvertirControles:
                StartCoroutine(InvertirControles());
                break;
            case TipoPowerUp.DispararMasRapido:
                StartCoroutine(DispararMasRapido());
                break;
            case TipoPowerUp.MoverseMasRapido:
                StartCoroutine(MoverseMasRapido());
                break;
        }
    }

    void RepararBunkers()
    {
        /*// Encuentra todos los b�nkers en la escena y los "repara" restaurando su vida
        Bunker[] bunkers = FindObjectsOfType<Bunker>();
        foreach (Bunker bunker in bunkers)
        {
            bunker.vidas = 5; // Restaura las vidas del b�nker
        }
        Debug.Log("�Todos los b�nkers han sido reparados!");*/
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
