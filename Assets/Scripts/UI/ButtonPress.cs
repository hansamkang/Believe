using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool pressing = false;
    public bool pressUp = false;

    void OnDisable()
    {
        pressUp = false;
        pressing = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressUp = false;
        pressing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressUp = true;
        pressing = false;
    }
}
