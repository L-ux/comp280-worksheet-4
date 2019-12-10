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
        Vector3 location = player.position + cameraDir.transform.forward.normalized * 3;

        Vector3 toEnemy3 = enemy.position - location;

        float dotdot = Vector3.Dot(cameraDir.transform.forward,toEnemy3);
        bool isUp = Vector3.Cross(cameraDir.transform.forward,toEnemy3).y > 0;

        radarMat.SetVector("_RadarLocation", new Vector4(location.x,0,location.z,0));
        radarMat.SetVector("_EnemyDirection", new Vector4(toEnemy3.x,0,toEnemy3.z,0));
        radarMat.SetVector("_CameraDirection", new Vector4(cameraDir.transform.forward.x, 0, cameraDir.transform.forward.z, 0));
        radarMat.SetFloat("_EnemyAngle", dotdot);
        radarMat.SetFloat("_EnemyIsRight", isUp ? 1 : 0);
    }
}
