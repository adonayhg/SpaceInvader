using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject BotonJugar;
    public GameObject BotonOptions;
    public GameObject BotonExit;
    public GameObject PopUpOptions;
    public GameObject Sliders;
    public GameObject Logo;

    [SerializeField] Slider sliderMusica;
    [SerializeField] float volumenMusica;
    [SerializeField] float recordScore;


    void Start()
    {
        // Animación inicial de los botones
        LeanTween.moveY(BotonJugar, 180, 0.5f);
        LeanTween.moveY(BotonOptions, 135, 0.5f);
        LeanTween.moveY(BotonExit, 90, 0.5f);

        // Configuración inicial del volumen
        sliderMusica.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        if (musicManager != null)
        {
            musicManager.SetVolume(sliderMusica.value);
        }
        recordScore = PlayerPrefs.GetInt("Player Score");

    }


    public void ChangeSlider(float valor)
    {
        volumenMusica = valor;
        PlayerPrefs.SetFloat("volumenAudio", volumenMusica);

        MusicManager musicManager = FindObjectOfType<MusicManager>();
        if (musicManager != null)
        {
            musicManager.SetVolume(sliderMusica.value);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("MiniJuego");
    }

    public void Options()
    {
        PopUpOptions.SetActive(true);
        LeanTween.scaleX(PopUpOptions, 1, 0.1f);
    }

    public void CerrarOptions()
    {
        PopUpOptions.SetActive(false);
        LeanTween.scaleX(PopUpOptions, 0, 0.1f);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Next()
    {
        SceneManager.LoadScene("Minijuego");
    }

    public void ReStart()
    {
        SceneManager.LoadScene("Minijuego");
    }
}

