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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Nave1"))
        {
            SceneManager.LoadScene("JuegoNave1");
        }
    }
}
