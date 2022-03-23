using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //oyun objelerini game object deðilde UI olarak kullanacaksak UI kütüphanesini içeri almamýz gerekiyor

public class Puan : MonoBehaviour
{
    int puan;
    int enYuksekPuan;

    int altin;
    int enYuksekAltin;

    bool puanTopla = true;

    [SerializeField]
    Text puanText = default;

    [SerializeField]
    Text altinText = default;

    [SerializeField]
    Text OyunBittiPuanText = default;

    [SerializeField]
    Text OyunBittiAltinText = default;

    // Start is called before the first frame update
    void Start()
    {

        altinText.text = " X " + altin;

    }

    // Update is called once per frame
    void Update()
    {
        if (puanTopla)
        {
            puan = (int)Camera.main.transform.position.y;
            puanText.text = "Puan: " + puan;
        }
       
    }

    public void AltinKazan()
    {
            FindObjectOfType<SesKontrol>().AltinSes();
             
            altin++;
            altinText.text = " X " + altin;
    }
    public void OyunBitti()
    {
        if (Secenekler.KolayDegerOku() == 1)
        {
            enYuksekPuan = Secenekler.KolayPuanDegerOku();
            enYuksekAltin = Secenekler.KolayAltinDegerOku();
            if (puan > enYuksekPuan)
            {
                Secenekler.KolayPuanDegerAta(puan);
            }
            if (altin > enYuksekAltin)
            {
                Secenekler.KolayAltinDegerAta(altin);
            }
        }

        if (Secenekler.OrtaDegerOku() == 1)
        {
            enYuksekPuan = Secenekler.OrtaPuanDegerOku();
            enYuksekAltin = Secenekler.OrtaAltinDegerOku();
            if (puan > enYuksekPuan)
            {
                Secenekler.OrtaPuanDegerAta(puan);
            }
            if (altin > enYuksekAltin)
            {
                Secenekler.OrtaAltinDegerAta(altin);
            }
        }

        if (Secenekler.ZorDegerOku() == 1)
        {
            enYuksekPuan = Secenekler.ZorPuanDegerOku();
            enYuksekAltin = Secenekler.ZorAltinDegerOku();
            if (puan > enYuksekPuan)
            {
                Secenekler.ZorPuanDegerAta(puan);
            }
            if (altin > enYuksekAltin)
            {
                Secenekler.ZorAltinDegerAta(altin);
            }
        }
        OyunBittiPuanText.text = "Puan: " + puan;
        OyunBittiAltinText.text = " X " + altin;

    }
} 
