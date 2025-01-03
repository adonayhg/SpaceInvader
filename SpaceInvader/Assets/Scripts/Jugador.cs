using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float speed = 20f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0/*, vertical*/);

        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Disparar();
        }
    }

    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public Transform puntoDisparo2;



    void Disparar()
    {
        GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);
        GameObject proyectil2 = Instantiate(proyectilPrefab, puntoDisparo2.position, puntoDisparo2.rotation);
    }
}
