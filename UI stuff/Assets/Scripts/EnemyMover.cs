using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public float distance;
    public float speed;
    float dTime = 0.0f;


    // Update is called once per frame
    void Update()
    {
        dTime += Time.deltaTime;

        transform.position = new Vector3(distance * Mathf.Sin(dTime * speed),1.103f, distance * Mathf.Cos(dTime * speed));
    }
}
