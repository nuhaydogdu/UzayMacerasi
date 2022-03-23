using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EkranHesaplayicisi : MonoBehaviour                  
{                                                                   //BUNU NASIL S�NGLETON YAPACA�IZ
    public static EkranHesaplayicisi instance;       //�ncelikle ayn� t�rde instance isimli static obje tan�ml�yoruz
                                                    //instance olarak olu�turdugumuz bu obje diger s�n�flar i�erisinde kullanabilece�imiz bir S�NGLETON olacak. 


    float yukseklik;
    float genislik;

    public float Yukseklik              //  bunlara d��ardan ula�abilmek i�in yaz�yoruz 
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

    void Awake()  // �steki static instance ve awake metodunun i�inde destroy k�sm�n�n dail oldu�u yere kadar singleton yapmka i�in yeterli 
    {
        if (instance == null)     
        {
            instance = this;           //instance objenin kendisi oluyor.
        }                                   
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //-di�erleri bize bu instancenin neler hesaplayacag�n� yazmak i�in
        yukseklik = Camera.main.orthographicSize;
        genislik = yukseklik * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
