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

        Vector3 location = player.position + camtransform.normalized * 3;

        Vector3 toEnemy3 = enemy.position - location;


        radarMat.SetVector("_RadarLocation", new Vector4(location.x,0,location.z,0));
        //radarMat.SetVector("_EnemyDirection", new Vector4(toEnemy3.x,0,toEnemy3.z,0));
        //radarMat.SetVector("_CameraDirection", new Vector4(camtransform.x, 0, camtransform.z, 0));

        Vector2 toEnemy2 = new Vector2(toEnemy3.x, toEnemy3.z);
        Vector2 camdir2 = new Vector2(camtransform.x, camtransform.z);


        float dotCamEnemy = Vector2.Dot(toEnemy2.normalized, new Vector2(1,0));

        //dotCamEnemy = ((-dotCamEnemy)+1)*Mathf.PI;

        radarMat.SetFloat("_EnemyAngle", dotCamEnemy);
        Debug.Log(dotCamEnemy);
        radarMat.SetInt("_EnemyRight", Vector3.Cross(camtransform,new Vector3(1,0,0)).y > 0 ? 1 : 0);

    }
}
