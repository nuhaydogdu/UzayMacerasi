using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //- OnPointerDown metodun ald��� parametreleri alabilmesi i�in bunu ekledik

public class JoystickButon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler // bu interface leri kullanarak ekrandaki buton ile karakterlerin z�playabilmesini sa�lad�k
{
    [HideInInspector]  // de�i�ken public olsada inspecturede gizlemek i�in kullan�l�yor
    public bool tusaBasildi; // public olmal� hareket kontrolden buna eri�ebiilmemiz i�in

    public void OnPointerDown(PointerEventData eventData)
    {

        tusaBasildi = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        tusaBasildi = false;
    }
}
