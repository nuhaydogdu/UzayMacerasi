using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EkranHesaplayicisi : MonoBehaviour                  
{                                                                   //BUNU NASIL SÝNGLETON YAPACAÐIZ
    public static EkranHesaplayicisi instance;       //Öncelikle ayný türde instance isimli static obje tanýmlýyoruz
                                                    //instance olarak oluþturdugumuz bu obje diger sýnýflar içerisinde kullanabileceðimiz bir SÝNGLETON olacak. 


    float yukseklik;
    float genislik;

    public float Yukseklik              //  bunlara dýþardan ulaþabilmek için yazýyoruz 
    {
        get
        {
            return yukseklik;
        }
    }
    public float Genislik
    {
        get
        {
            return genislik;
        }
    }

    void Awake()  // üsteki static instance ve awake metodunun içinde destroy kýsmýnýn dail olduðu yere kadar singleton yapmka için yeterli 
    {
        if (instance == null)     
        {
            instance = this;           //instance objenin kendisi oluyor.
        }                                   
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //-diðerleri bize bu instancenin neler hesaplayacagýný yazmak için
        yukseklik = Camera.main.orthographicSize;
        genislik = yukseklik * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
