using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    int totalColumns = 12;
    [SerializeField]
    int totalRow = 10;
    [SerializeField]
    float initialPosX = -7.8f;
    [SerializeField]
    float initialPosY = 14.07f;
    [SerializeField]
    float spaceBetweenElementsX = 2.25f;
    [SerializeField]
    float spaceBetweenElementsY = 1.5f;
    [SerializeField]
    GameObject prefab;
    List<List<GameObject>> matrizObjetos = new List<List<GameObject>>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < totalColumns; i++)
        {
            matrizObjetos.Add(new List<GameObject>());
            for (int j = 0; j < totalRow; j++)
            {
                Vector3 position = new Vector3(initialPosX, initialPosY, 0.0f);
                position.x = position.x + i * spaceBetweenElementsX;
                position.y = position.y - j * spaceBetweenElementsY;
                GameObject alien = Instantiate(prefab, position, Quaternion.identity);
                alien.name = "Alien(" + i.ToString() + "," + j.ToString() + ")";
                matrizObjetos[i].Add(alien);
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            int randX = Random.Range(0, totalColumns);
            int randY = Random.Range(0, totalRow);
            matrizObjetos[randX][randY].SetActive(!matrizObjetos[randX][randY].activeSelf);
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            int randX = Random.Range(0, totalColumns);
            int ultimoElementoActivo = -1;
            for (int i = 0; i < totalRow; i++)
            {
                if (matrizObjetos[randX][i].activeSelf == true)
                {
                    ultimoElementoActivo = i;
                }
            }
            Debug.Log("Ultimo elemento activo " + randX + ", " + ultimoElementoActivo);
        }
    }
}
