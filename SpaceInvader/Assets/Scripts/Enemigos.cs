using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemigos : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array para los prefabs de enemigos
    public int filas = 3;
    public int columnas = 5;
    public float velocidadInicial = 2f;
    public float aumentoVelocidad = 0.1f;
    public float distanciaMovimientos = 0.5f;
    public float intervaloDisparo = 1.0f;
    public Vector2 espacioEnemigos = new Vector2(1.0f, 1.0f); // Espacio entre enemigos (x: columnas, y: filas)
    public LayerMask wallLayer; // LayerMask para las paredes

    private GameObject[,] enemigos;
    private float direccionMovimiento = 1.0f; // 1 para derecha, -1 para izquierda
    private float velocidadActual;
    private int enemigosRestantes;
    private bool debeCaer = false;

    public GameObject proyectilPrefab; // Prefab del proyectil

    void Start()
    {
        velocidadActual = velocidadInicial;
        InicioEnemigos();
        InvokeRepeating(nameof(MetodoDisparoEnemigo), intervaloDisparo, intervaloDisparo);
    }

    void Update()
    {
        MovimientoEnemigos();
    }

    void InicioEnemigos()
    {
        enemigos = new GameObject[filas, columnas];
        enemigosRestantes = filas * columnas;

        // Definir la posición inicial base para los enemigos
        float startX = -38f;
        float startY = 3f;
        float startZ = 8f;

        for (int fila = 0; fila < filas; fila++)
        {
            for (int columna = 0; columna < columnas; columna++)
            {
                Vector3 posicion = new Vector3(
                    startX + columna * espacioEnemigos.x - (columnas - 1) * espacioEnemigos.x / 2.0f,
                    startY,
                    startZ + fila * espacioEnemigos.y
                );

                int tipoEnemigo = Random.Range(0, enemyPrefabs.Length);
                GameObject enemigo = Instantiate(enemyPrefabs[tipoEnemigo], posicion, Quaternion.identity);

                if (tipoEnemigo == 0)
                {
                    enemigo.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    enemigo.transform.rotation = Quaternion.Euler(0, -90, 90);

                }
                if (tipoEnemigo == 1)
                {
                    enemigo.transform.localScale = new Vector3(0.36f, 0.36f, 0.36f);
                }
                if (tipoEnemigo == 2)
                {
                    enemigo.transform.rotation = Quaternion.Euler(0, -180, 0);
                    enemigo.transform.position = new Vector3(
                        enemigo.transform.position.x,
                        0, // Posición Y fija en 0
                        enemigo.transform.position.z
                    );
                }
                if (tipoEnemigo == 3)
                {
                    enemigo.transform.rotation = Quaternion.Euler(0, -180, 0);

                }


                enemigos[fila, columna] = enemigo;
            }
        }
    }

    void MovimientoEnemigos()
    {
        float pasoMovimiento = velocidadActual * Time.deltaTime;

        foreach (GameObject enemigo in enemigos)
        {
            if (enemigo != null)
            {
                // Verificar si hay colisión con la pared antes de mover
                if (!IsAtWall(enemigo.transform.position, direccionMovimiento))
                {
                    enemigo.transform.position += Vector3.right * direccionMovimiento * pasoMovimiento;
                }
                else
                {
                    debeCaer = true;
                }

                Vector3 posicionPantalla = Camera.main.WorldToScreenPoint(enemigo.transform.position);
                if ((direccionMovimiento > 0 && posicionPantalla.x >= Screen.width) || (direccionMovimiento < 0 && posicionPantalla.x <= 0))
                {
                    debeCaer = true;
                }
            }
        }

        if (debeCaer)
        {
            foreach (GameObject enemigo in enemigos)
            {
                if (enemigo != null)
                {
                    enemigo.transform.position += new Vector3(0, 0, -1) * distanciaMovimientos;
                }
            }
            direccionMovimiento *= -1;
            debeCaer = false;
        }
    }

    bool IsAtWall(Vector3 posicion, float direccion)
    {
        // Realizar un raycast para verificar si hay una pared delante
        Vector3 direccionRayo = direccion > 0 ? Vector3.right : Vector3.left;
        RaycastHit hit;
        if (Physics.Raycast(posicion, direccionRayo, out hit, 1f, wallLayer))
        {
            return true; // Pared detectada
        }
        return false;
    }

    void MetodoDisparoEnemigo()
    {
        for (int columna = 0; columna < columnas; columna++)
        {
            // Recorrer las filas desde la fila más baja hacia arriba
            for (int fila = 0; fila < filas; fila++)
            {
                if (enemigos[fila, columna] != null)
                {
                    // Esperar un tiempo aleatorio antes de disparar
                    StartCoroutine(DisparoConRetraso(enemigos[fila, columna], Random.Range(0.1f, 5f)));
                    break; // Salir del bucle para esta columna después de que el enemigo más bajo dispare
                }
            }
        }
    }

    IEnumerator DisparoConRetraso(GameObject enemigo, float retraso)
    {
        yield return new WaitForSeconds(retraso);

        if (enemigo != null) // Verificar si el enemigo sigue vivo antes de disparar
        {
            DisparoEnemigo(enemigo);
        }
    }

    void DisparoEnemigo(GameObject enemigo)
    {
        // Usar el centro del enemigo como la posición de disparo
        Vector3 posicionDisparo = enemigo.transform.position;

        // Instanciar el proyectil en la posición del centro del enemigo
        Instantiate(proyectilPrefab, posicionDisparo, Quaternion.identity);
    }


    void EnemigoDestruido(GameObject enemigo)
    {
        for (int fila = 0; fila < filas; fila++)
        {
            for (int columna = 0; columna < columnas; columna++)
            {
                if (enemigos[fila, columna] == enemigo)
                {
                    enemigos[fila, columna] = null;
                    enemigosRestantes--;

                    // Intentar soltar un power-up
                    FindObjectOfType<PowerUpManager>().IntentarSoltarPowerUp(enemigo.transform.position);

                    velocidadActual += aumentoVelocidad;
                    Destroy(enemigo);
                    return;
                }
            }
        }
    }
}