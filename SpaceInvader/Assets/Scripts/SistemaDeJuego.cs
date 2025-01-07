using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SistemaDeJuego : MonoBehaviour
{
    public static SistemaDeJuego instancia; // Singleton para acceso global

    public int puntos = 0; // Puntos acumulados del jugador
    public int vidas = 5;  // Vidas iniciales de la nave
    public TMP_Text textoPuntos; // Texto en la UI para mostrar los puntos (opcional)
    public TMP_Text textoVidas;  // Texto en la UI para mostrar las vidas (opcional)
    public GameObject pantallaGameOver;

    void Awake()
    {
        // Configurar el singleton
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ActualizarUI();
        pantallaGameOver.SetActive(false);
    }

    // Método para sumar puntos al jugador
    public void AgregarPuntos(int cantidad)
    {
        puntos += cantidad;
        ActualizarUI();
    }

    // Método para reducir las vidas del jugador
    public void PerderVida()
    {
        vidas--;
        ActualizarUI();

        if (vidas <= 0)
        {
            pantallaGameOver.SetActive(true);
        }
    }

    // Actualiza la UI
    void ActualizarUI()
    {
        if (textoPuntos != null)
        {
            textoPuntos.text = "Puntos: " + puntos;
        }
        if (textoVidas != null)
        {
            textoVidas.text = "Vidas: " + vidas;
        }
    }

}

