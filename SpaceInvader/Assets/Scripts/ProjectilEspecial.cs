using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectilEspecial : MonoBehaviour
{
    public float speed = 10f;
    Vector3 direccion = new Vector3(0, 0, 1);
    Vector3 tamaño = new Vector3(5, 5, 5);
    public GameObject Collider;
    public GameObject proyectilEspecial;
    public static ProjectilEspecial instancia; // Singleton para acceso global

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
        Collider.GetComponent<SphereCollider>().enabled = false;

    }
    void Update()
    {
        transform.Translate(direccion * speed * Time.deltaTime);
        Collider.transform.position = gameObject.transform.position;
    }
    public void RadioProyectil()
    {
        Collider.GetComponent<SphereCollider>().enabled = true;
    }
}
