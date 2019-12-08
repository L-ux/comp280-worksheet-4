using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public Canvas pauseCanvas;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitBecomeActive());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator waitBecomeInactive()
    {
        yield return new WaitUntil(() => MenuController.currentMenu != MenuController.Menus.Pause);
        pauseCanvas.gameObject.SetActive(false);
        StartCoroutine(waitBecomeActive());
    }

    IEnumerator waitBecomeActive()
    {
        yield return new WaitUntil(() => MenuController.currentMenu == MenuController.Menus.Pause);
        pauseCanvas.gameObject.SetActive(true);
        StartCoroutine(waitBecomeInactive());
    }
}