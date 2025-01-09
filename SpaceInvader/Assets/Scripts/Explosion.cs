using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Explosion : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
