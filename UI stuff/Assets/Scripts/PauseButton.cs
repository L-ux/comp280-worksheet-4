using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public MenuController.Menus targetMenu;

    public Sprite normalImage;
    public Sprite hoverImage;


    public void OnPointerDown(PointerEventData eventData)
    {
        MenuController.changeMenu(targetMenu);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().sprite = hoverImage;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().sprite = normalImage;
    }
}
