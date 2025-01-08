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


    // Start is called before the first frame update
    void Start()
    {
        LeanTween.moveY(BotonJugar, 180, 0.5f);
        LeanTween.moveY(BotonOptions, 135, 0.5f);
        LeanTween.moveY(BotonExit, 90, 0.5f);
        sliderMusica.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = sliderMusica.value;
    }

    // Update is called once per frame
    void Update()
    {
        
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

    [SerializeField] Slider sliderMusica;
    [SerializeField] float volumenMusica;

    public void ChangeSlider(float valor)
    {
        volumenMusica = valor;
        PlayerPrefs.SetFloat("volumenAudio", volumenMusica);
        AudioListener.volume = sliderMusica.value;
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
