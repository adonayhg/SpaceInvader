using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SistemaDeJuego : MonoBehaviour
{
    public static SistemaDeJuego instancia; // Singleton para acceso global

    public int puntos = 0; // Puntos acumulados del jugador
    public int vidas = 5;  // Vidas iniciales de la nave
    public Text textoPuntos; // Texto en la UI para mostrar los puntos (opcional)
    public Text textoVidas;  // Texto en la UI para mostrar las vidas (opcional)

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
            // Reiniciar la escena si se terminan las vidas
            Debug.Log("¡Juego Terminado!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

