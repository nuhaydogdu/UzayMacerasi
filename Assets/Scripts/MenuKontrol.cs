using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //müzük kapalý için ekleyecek olduðumuz objenin buton olarak tanýna bilmesi için
using UnityEngine.SceneManagement;     //Unityde sahneler arasý geçiþ yapmamýzý saðlýyor

public class MenuKontrol : MonoBehaviour
{
    [SerializeField]
    Sprite[] muzikIkonlar = default;

    [SerializeField]                                    // muzik butonunun spraytýný deðiþtireceðimiz için bu scriptin o butonuda tanýyor olmasý lazým !!!!!!!!!!!
    Button muzikButon = default;                      // UI yazamasak bu button classýný kullanamazdým

    

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
    public void OyunuBaslat()         // oyunu baþlat düðmesini seçip ((((on click))) i seçip menü kontrol objesiye beraber ekliyoruz oraya
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
