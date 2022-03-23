using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //- OnPointerDown metodun aldýðý parametreleri alabilmesi için bunu ekledik

public class JoystickButon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler // bu interface leri kullanarak ekrandaki buton ile karakterlerin zýplayabilmesini saðladýk
{
    [HideInInspector]  // deðiþken public olsada inspecturede gizlemek için kullanýlýyor
    public bool tusaBasildi; // public olmalý hareket kontrolden buna eriþebiilmemiz için

    public void OnPointerDown(PointerEventData eventData)
    {

        tusaBasildi = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        tusaBasildi = false;
    }
}
