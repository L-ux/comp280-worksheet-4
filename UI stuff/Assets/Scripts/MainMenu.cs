using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image[] Radar;   // all radar sprites
    public Image[] HP;      // all enemy sprites
    public Image[] Mags;    // all magazine sprites
    public Text AmmoText;   // the ammo numbers

    public Canvas staticCanvas; // for pausing n such
    public Canvas dynamicCanvas;


    public Transform Enemy; // enemy for the radar, below stuff for enemy details
    Vector3 toEnemy3;
    float radAngle;
    Vector3 crossyboi; 

    // STARTING VALUES / COUNTING VALUES
    int iHP = 15; // Players HP
    int iMags = 6; // number of extra mags the player has
    int iMagCap = 30; // number of rounds in a mag
    int iAmmo = 30; // number of round in your CURRENT mag

    public bool isActive = true;

    // fancy stuff for directions
    enum e_Dirs
    {
        North,      // 0 , 4
        NorthEast,  // 0 , 1
        East,       // 1 , 2
        SouthEast,  // 2 , 3
        South,      // 3 , 7
        SouthWest,  // 6 , 7
        West,       // 5 , 6
        NorthWest,  // 4 , 5

        NNE,     // 0
        ENE,     // 1
        ESE,     // 2
        SSE,     // 3
        NNW,     // 4
        WNW,     // 5
        WSW,     // 6
        SSW      // 7
    }
    e_Dirs currentDir = e_Dirs.SSW;
    List<int> Directions = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        HPChange(iHP);   // set first HP bar visible
        AmmoChange(iAmmo);
        MagChange(iMags);
        DirChange();
        StartCoroutine(waitBecomeInactive());
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
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
                if (iMags > 0 && iAmmo < 30)
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

            e_Dirs wat = doTheBigAngleCheck(radAngle);
            if (wat != currentDir)
                currentDir = wat;
            #endregion   
        }      
    }




    e_Dirs doTheBigAngleCheck(float angle)
    {
        if (angle % (Mathf.PI / 4) == 0) // any 45 degree angle
        {
            if (angle == 0)
            {
                return e_Dirs.North;
            }
            else if (angle == Mathf.PI)
            {
                return e_Dirs.South;
            }
            else
            {
                int tempDir = (int)(angle / (Mathf.PI / 4));
                if (tempDir == 1)
                {
                    return crossyboi.y > 0 ? e_Dirs.NorthEast : e_Dirs.NorthWest;
                }
                else if (tempDir == 2)
                {
                    return crossyboi.y > 0 ? e_Dirs.East : e_Dirs.West;
                }
                else if (tempDir == 3)
                {
                    return crossyboi.y > 0 ? e_Dirs.SouthEast : e_Dirs.SouthWest;
                }
            }
        }
        else 
        {
            int tempDir = (int)(angle / (Mathf.PI / 4));
            if (tempDir == 0)
            {
                return crossyboi.y > 0 ? e_Dirs.NNE : e_Dirs.NNW;
            }
            else if (tempDir == 1)
            {
                return crossyboi.y > 0 ? e_Dirs.ENE : e_Dirs.WNW;
            }
            else if (tempDir == 2)
            {
                return crossyboi.y > 0 ? e_Dirs.ESE : e_Dirs.WSW;
            }
            else if (tempDir == 3)
            {
                return crossyboi.y > 0 ? e_Dirs.SSE : e_Dirs.SSW;
            }
        }

        return e_Dirs.North;
    }


    IEnumerator waitHPChange(int hp)
    {
        yield return new WaitUntil(() => iHP != hp); // wait until HP changes
        HPChange(hp); // then tell to update something with the HP
    }

    IEnumerator waitAmmoChange(int ammo)
    {
        yield return new WaitUntil(() => iAmmo != ammo); // wait until ammo changes
        AmmoChange(ammo); // then update ammo
    }

    IEnumerator waitMagsChange(int mags)
    {
        yield return new WaitUntil(() => iMags != mags); // wait until no. of mags changes
        MagChange(mags); // then update mags
    }

    IEnumerator waitDirectionChange(e_Dirs pointy)
    {
        yield return new WaitUntil(() => pointy != currentDir);
        DirChange();
    }


    IEnumerator waitBecomeInactive()
    {
        yield return new WaitUntil(() => MenuController.currentMenu != MenuController.Menus.Game);
        staticCanvas.gameObject.SetActive(false);
        dynamicCanvas.gameObject.SetActive(false);
        isActive = false;
        StartCoroutine(waitBecomeActive());
    }

    IEnumerator waitBecomeActive()
    {
        yield return new WaitUntil(() => MenuController.currentMenu == MenuController.Menus.Game);
        staticCanvas.gameObject.SetActive(true);
        dynamicCanvas.gameObject.SetActive(true);
        isActive = true;
        StartCoroutine(waitBecomeInactive());
    }

    void HPChange(int oldhp)
    {   
        if (oldhp != 0)
            HP[oldhp-1].gameObject.SetActive(false);
        if (iHP != 0)
            HP[iHP-1].gameObject.SetActive(true);
        StartCoroutine(waitHPChange(iHP));
    }

    void AmmoChange(int oldAmmo)
    {
        AmmoText.text = iAmmo.ToString();
        StartCoroutine(waitAmmoChange(iAmmo));
    }

    void MagChange(int oldMags)
    {
        if (oldMags != 0)
            Mags[oldMags - 1].gameObject.SetActive(false);
        if (iMags != 0)
            Mags[iMags - 1].gameObject.SetActive(true);
        StartCoroutine(waitMagsChange(iMags));
    }

    void DirChange()
    {
        for (int i = 0; i < 8; i++)
        {
            Radar[i].gameObject.SetActive(false);
        }

        // could be done A LOT nicer, but oh well

        if (currentDir == e_Dirs.North || currentDir == e_Dirs.NorthEast || currentDir == e_Dirs.NNE)
        {
            Radar[0].gameObject.SetActive(true);
        }
        if (currentDir == e_Dirs.NorthEast || currentDir == e_Dirs.East || currentDir == e_Dirs.ENE)
        {
            Radar[1].gameObject.SetActive(true);
        }
        if (currentDir == e_Dirs.East || currentDir == e_Dirs.SouthEast || currentDir == e_Dirs.ESE)
        {
            Radar[2].gameObject.SetActive(true);
        }
        if (currentDir == e_Dirs.SouthEast || currentDir == e_Dirs.South || currentDir == e_Dirs.SSE)
        {
            Radar[3].gameObject.SetActive(true);
        }
        if (currentDir == e_Dirs.North || currentDir == e_Dirs.NorthWest || currentDir == e_Dirs.NNW)
        {
            Radar[4].gameObject.SetActive(true);
        }
        if (currentDir == e_Dirs.NorthWest || currentDir == e_Dirs.West || currentDir == e_Dirs.WNW)
        {
            Radar[5].gameObject.SetActive(true);
        }
        if (currentDir == e_Dirs.West || currentDir == e_Dirs.SouthWest || currentDir == e_Dirs.WSW)
        {
            Radar[6].gameObject.SetActive(true);
        }
        if (currentDir == e_Dirs.SouthWest || currentDir == e_Dirs.South || currentDir == e_Dirs.SSW)
        {
            Radar[7].gameObject.SetActive(true);
        }

        StartCoroutine(waitDirectionChange(currentDir));
    }
}