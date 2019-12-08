using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour, IPointerDownHandler
{
    public MenuController.Menus targetMenu;

    public void OnPointerDown(PointerEventData eventData)
    {
        MenuController.changeMenu(targetMenu);
    }
}
