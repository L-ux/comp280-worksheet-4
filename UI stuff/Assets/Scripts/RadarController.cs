using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarController : MonoBehaviour
{
    public Material radarMat;

    public Transform player;
    public Transform enemy;
    public Transform cameraDir;


    void Update()
    {
        Vector3 camtransform = cameraDir.transform.forward;

        Vector3 radarLocation = player.position + camtransform.normalized * 3;

        Vector3 toEnemy3 = enemy.position - radarLocation;
        Vector2 toEnemy2 = new Vector2(toEnemy3.x, toEnemy3.z).normalized;

        radarMat.SetVector("_RadarLocation", new Vector4(radarLocation.x,0,radarLocation.z,0));
        radarMat.SetVector("_EnemyDirection", new Vector4(toEnemy2.x,0,toEnemy2.y,0));
    }
}
