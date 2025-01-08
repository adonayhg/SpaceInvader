using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Proyectil : MonoBehaviour
{
    public float speed = 10f;
    Vector3 direccion = new Vector3(0, 0, 1);
    void Start()
    {

    }
    void Update()
    {
        transform.Translate(direccion * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Nave1"))
        {
            SceneManager.LoadScene("JuegoNave1");
        }
        if (other.gameObject.CompareTag("Nave2"))
        {
            SceneManager.LoadScene("JuegoNave2");
        }
        if (other.gameObject.CompareTag("Nave3"))
        {
            SceneManager.LoadScene("JuegoNave3");
        }
    }
}
