using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltinAlgilayici : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ayaklar")
        {
            GetComponentInParent<Altin>().AltiniKapat();  // altýn scriptimiz bu algýlayýcýnýn ekli olduðu objenin parentinde, ona ulaþýyoruz. 
            FindObjectOfType<Puan>().AltinKazan();

        }
    }
}
