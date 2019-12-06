using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image[] Radar;
    public Image[] HP;
    public Image[] Mags;
    public Text Ammo;

    public Image test;

    public Transform Enemy;
    Vector3 toEnemy3;
    float radAngle;
    Vector3 crossyboi; 
    bool enemyToLeft;

    int iHP = 15;
    int iMags = 6;
    int iMagCap = 30;
    int iAmmo = 30;

    // Start is called before the first frame update
    void Start()
    {
        HPChange(15);   // set first HP bar visible
        StartCoroutine(waitHPChange(iHP));  // start waiting for HP change
    }

    // Update is called once per frame
    void Update()
    {
        #region HP
        if(Input.GetKeyDown(KeyCode.DownArrow)) // lose HP
        {
            if (iHP != 0)
                iHP--;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow)) // gain HP
        {
            if (iHP !=15)
                iHP++;
        }
        #endregion
        #region Ammo
        else if(Input.GetKeyDown(KeyCode.Mouse0)) // fire
        {
            if (iAmmo > 0)
            {
                Debug.Log("pew pew");
                iAmmo--;
            }
        }
        else if(Input.GetKeyDown(KeyCode.R)) // reload
        {
            if (iMags > 0)
            {
                iAmmo = iMagCap;
                iMags--;
            }
        }
        #endregion
        #region Enemy
        toEnemy3 = Enemy.transform.position - transform.position;
        radAngle = Mathf.Acos(Vector3.Dot(transform.forward, toEnemy3) / (transform.forward.magnitude * toEnemy3.magnitude));
        crossyboi = Vector3.Cross(transform.forward, toEnemy3); // enemy to right is +ve
        if (crossyboi.y > 0)
            enemyToLeft = false;
        else if (crossyboi.y < 0)
            enemyToLeft = true;
        else 
            Debug.Log("0/180"); // check the dot product to tell whether is forward/backward

        

            
        #endregion         
    }



    IEnumerator waitHPChange(int hp)
    {
        yield return new WaitUntil(() => iHP != hp); // wait until HP changes
        HPChange(hp); // then tell to update something with the HP
    }

    void HPChange(int oldhp)
    {   
        if (oldhp != 0)
        HP[oldhp-1].gameObject.SetActive(false);
        if (iHP != 0)
            HP[iHP-1].gameObject.SetActive(true);
        StartCoroutine(waitHPChange(iHP));
    }
}
