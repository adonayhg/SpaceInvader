using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [SerializeField] GameObject interfazUsuario;
    [SerializeField] GameObject popUpOpciones;
    public void Jugar()
    {
        interfazUsuario.SetActive(false);
    }
    public void Opciones()
    {
        popUpOpciones.SetActive(true);
    }
    public void Salir()
    {

    }
    public void CerrarOpciones()
    {
        popUpOpciones.SetActive(false);
    }
}
