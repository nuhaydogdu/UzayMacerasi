using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltinAlgilayici : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ayaklar")
        {
            GetComponentInParent<Altin>().AltiniKapat();  // alt�n scriptimiz bu alg�lay�c�n�n ekli oldu�u objenin parentinde, ona ula��yoruz. 
            FindObjectOfType<Puan>().AltinKazan();

        }
    }
}
