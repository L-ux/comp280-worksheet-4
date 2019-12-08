using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public enum Menus
    {
        Game,
        Pause,
        Settings
    }
    public static Menus currentMenu;


    // Start is called before the first frame update
    void Awake()
    {
        currentMenu = Menus.Game;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (currentMenu == Menus.Game)
            {
                currentMenu = Menus.Pause;
            }
            else if (currentMenu == Menus.Pause)
            {
                currentMenu = Menus.Game;
            }
            else if (currentMenu == Menus.Settings)
            {
                currentMenu = Menus.Pause;
            }
        }
    }

    public static void changeMenu(Menus menuToChangeTo)
    {
        currentMenu = menuToChangeTo;
    }


}
