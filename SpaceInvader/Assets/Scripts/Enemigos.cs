using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigos : MonoBehaviour
{
    private float movementSpeed = 2.0f; // Velocidad de movimiento
    private bool movingForward = true; // Dirección del movimiento en Z

    [SerializeField]
    int totalColumns = 5; // Número de columnas
    [SerializeField]
    int totalRows = 3; // Número de filas
    [SerializeField]
    float initialPosX = -7.5f; // Posición inicial en X
    [SerializeField]
    float initialPosZ = 10.0f; // Posición inicial en Z
    [SerializeField]
    float spaceBetweenElementsX = 2.5f; // Espaciado entre enemigos en X
    [SerializeField]
    float spaceBetweenElementsZ = 2.0f; // Espaciado entre enemigos en Z
    [SerializeField]
    GameObject[] enemyPrefabs; // Array de modelos de enemigos (diferentes prefabs)

    private List<List<GameObject>> enemyMatrix = new List<List<GameObject>>(); // Matriz para almacenar enemigos

    void Start()
    {
        GenerarEnemigos();
    }

    void GenerarEnemigos()
    {
        // Crear la cuadrícula de enemigos
        for (int column = 0; column < totalColumns; column++)
        {
            enemyMatrix.Add(new List<GameObject>());
            for (int row = 0; row < totalRows; row++)
            {
                // Calcular la posición de cada enemigo
                Vector3 position = new Vector3(
                    initialPosX + column * spaceBetweenElementsX, // Distribución en X
                    0.0f,                                        // Altura constante en Y
                    initialPosZ - row * spaceBetweenElementsZ   // Distribución en Z
                );

                // Seleccionar un prefab de enemigo aleatoriamente
                int prefabIndex = Random.Range(0, enemyPrefabs.Length);
                GameObject enemyPrefab = enemyPrefabs[prefabIndex];

                // Instanciar y nombrar el enemigo
                GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
                enemy.name = $"Enemy({column},{row})";

                // Asignar como hijo del controlador
                enemy.transform.parent = this.transform;

                // Agregar enemigo a la matriz
                enemyMatrix[column].Add(enemy);
            }
        }
    }

    void Update()
    {
        MoverEnemigos();
    }

    void MoverEnemigos()
    {
        // Mover a todos los enemigos en el eje Z
        float movementZ = movementSpeed * Time.deltaTime * (movingForward ? 1 : -1);
        transform.Translate(0, 0, movementZ);

        // Verificar los límites de movimiento
        foreach (Transform enemy in transform)
        {
            if (!enemy.gameObject.activeSelf) continue; // Ignorar enemigos desactivados

            // Si alcanzan un límite, cambiar de dirección
            if (movingForward && enemy.position.z >= 15.0f) // Límite superior en Z
            {
                CambiarDireccion();
                break;
            }
            else if (!movingForward && enemy.position.z <= 5.0f) // Límite inferior en Z
            {
                CambiarDireccion();
                break;
            }
        }
    }

    void CambiarDireccion()
    {
        movingForward = !movingForward; // Cambiar la dirección
    }

    public void DestruirEnemigo(int column, int row)
    {
        // Destruir o desactivar un enemigo específico en la matriz
        if (column >= 0 && column < totalColumns && row >= 0 && row < totalRows)
        {
            GameObject enemy = enemyMatrix[column][row];
            if (enemy != null)
            {
                enemy.SetActive(false);
                Debug.Log($"Enemigo destruido: {enemy.name}");
            }
        }
    }

    public Transform ObtenerEnemigoFilaInferior()
    {
        // Obtener un enemigo activo de la fila inferior de cada columna
        foreach (var column in enemyMatrix)
        {
            for (int i = column.Count - 1; i >= 0; i--) // Recorrer de abajo hacia arriba
            {
                if (column[i] != null && column[i].activeSelf)
                {
                    return column[i].transform; // Retorna el primer enemigo activo
                }
            }
        }
        return null;
    }
}
