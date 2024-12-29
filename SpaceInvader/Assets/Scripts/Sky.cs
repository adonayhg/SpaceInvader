using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.AudioSettings;
using UnityEngine.UIElements;

public class Sky : MonoBehaviour
{
    public Transform followTarget;

    Vector3 offset;

public GameObject starPrefab;

    public int numberstars;

    public float skyAltitude;

    public float maxDistance;
private void Start()
{
        offset = followTarget.transform.position;
        GenerateSky();
    }


void LateUpdate()
{
        transform.position = followTarget.position + offset;
}
    void GenerateSky()
    {
        float xPos,zPos;
        Vector3 pos;
        pos.y = skyAltitude;

        for(int i = 0; i < numberstars; i++)
        {
            pos = UnityEngine.Random.onUnitSphere * maxDistance;
            if (pos.y < 0)
            {
                pos.y = -pos.y;

            }
            GameObject s = Instantiate(starPrefab, pos, starPrefab.transform.rotation);
            s.transform.parent = transform;
            s.transform.LookAt(followTarget);
        }
    }

}
