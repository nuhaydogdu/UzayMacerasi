using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //m�z�k kapal� i�in ekleyecek oldu�umuz objenin buton olarak tan�na bilmesi i�in
using UnityEngine.SceneManagement;     //Unityde sahneler aras� ge�i� yapmam�z� sa�l�yor

public class MenuKontrol : MonoBehaviour
{
    [SerializeField]
    Sprite[] muzikIkonlar = default;

    [SerializeField]                                    // muzik butonunun sprayt�n� de�i�tirece�imiz i�in bu scriptin o butonuda tan�yor olmas� laz�m !!!!!!!!!!!
    Button muzikButon = default;                      // UI yazamasak bu button class�n� kullanamazd�m

    

    // Start is called before the first frame update
    void Start()
    {
        if (Secenekler.KayitVarmi() == false)
        {
            Secenekler.KolayDegerAta(1);
        }
        if (Secenekler.MuzikAcikKayitVarmi() == false)
        {
            Secenekler.MuzikAcikDegerAta(1);
        }
        MuzikAyarlariniDenetle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OyunuBaslat()         // oyunu ba�lat d��mesini se�ip ((((on click))) i se�ip men� kontrol objesiye beraber ekliyoruz oraya
    {
        SceneManager.LoadScene("Oyun");
    }
    public void EnYuksekPuan()   
    {
        SceneManager.LoadScene("Puan");
    }
    public void Ayarlar()
    {
        SceneManager.LoadScene("Ayarlar");
    }
    public void Muzik()
    {
        if (Secenekler.MuzikAcikDegerOku() == 1)
        {
            Secenekler.MuzikAcikDegerAta(0);
            MuzikKontrol.instance.MuzikCal(false); 
            muzikButon.image.sprite = muzikIkonlar[0];
        }
        else
        {
            Secenekler.MuzikAcikDegerAta(1);
            MuzikKontrol.instance.MuzikCal(true);
            muzikButon.image.sprite = muzikIkonlar[1];
        }
    }

    void MuzikAyarlariniDenetle()
    {
        if (Secenekler.MuzikAcikDegerOku() == 1)
        {
            muzikButon.image.sprite = muzikIkonlar[1];
            MuzikKontrol.instance.MuzikCal(true);
        }
        else
        {
            muzikButon.image.sprite = muzikIkonlar[0];
            MuzikKontrol.instance.MuzikCal(false);
        }
    }
}
