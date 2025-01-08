using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance; // Singleton para evitar duplicados
    private AudioSource audioSource;

    void Awake()
    {
        // Asegurar que este objeto no se destruya entre escenas
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Hace que el objeto persista
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject); // Destruye duplicados si ya existe uno
        }
    }

    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume; // Ajusta el volumen
        }
    }
}

