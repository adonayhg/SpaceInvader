using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public GameObject[] powerUpPrefabs; // Prefabs de los distintos power-ups
    public float dropChance = 30f; // Probabilidad de que un enemigo suelte un power-up (en %)

    // Método que llama un enemigo al ser destruido
    public void IntentarSoltarPowerUp(Vector3 posicion)
    {
        // Generar un número aleatorio entre 0 y 100
        float randomValue = Random.Range(0f, 100f);

        // Si el valor está dentro de la probabilidad definida, soltar un power-up
        if (randomValue <= dropChance)
        {
            // Seleccionar un power-up aleatorio del array
            int powerUpIndex = Random.Range(0, powerUpPrefabs.Length);
            Instantiate(powerUpPrefabs[powerUpIndex], posicion, Quaternion.identity);
        }
    }
}
