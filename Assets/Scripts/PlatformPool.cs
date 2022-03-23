using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{
    [SerializeField]
    GameObject platformPrefab = default;

    [SerializeField]
    GameObject olumculPlatformPrefab = default;

    [SerializeField]
    GameObject playerPrefab = default;

    List<GameObject> platforms = new List<GameObject>(); //oyunda kullanaca��m�z t�m plarformlar� tutan listimiz. 


    Vector2 platformPozisyon;
    Vector2 playerPozisyon;

    [SerializeField]
    float platformArasiMesafe = default;

    // Start is called before the first frame update
    void Start()
    {
        PlatformUret();
    }

    // Update is called once per frame
    void Update()
    {
        if (platforms[platforms.Count - 1].transform.position.y <           //Listemizin i�erisindeki son elaman�m�z�n y de�erini kameranin ye de�eriyle k�yasl�yoruz
            Camera.main.transform.position.y + EkranHesaplayicisi.instance.Yukseklik)
        {
            PlatformYerlestir();
        }
    }

    void PlatformUret()
    {
        platformPozisyon = new Vector2(0, 0);
        playerPozisyon = new Vector2(0, 0.5f);

        GameObject player = Instantiate(playerPrefab, playerPozisyon, Quaternion.identity);          //sahnede var etme i�lemi
        GameObject ilkPlatform = Instantiate(platformPrefab, platformPozisyon, Quaternion.identity);
        player.transform.parent = ilkPlatform.transform;  // aralar�nda parent child ili�kisi kurduk (player ve ilk platformu birbirine ba�lad�k gibi)
        platforms.Add(ilkPlatform);
        SonrakiPlatformPozisyon();
        ilkPlatform.GetComponent<Platform>().Hareket = false;

        for (int i = 0; i < 8; i++)
        {
            GameObject platform = Instantiate(platformPrefab, platformPozisyon, Quaternion.identity);
            platforms.Add(platform);
            platform.GetComponent<Platform>().Hareket = true; //polatformun hareket kontrol�n� sa�l�yoruz (hareket platform scriptinde tan�ml�).

            if (i % 2 == 0)
            {
                platform.GetComponent<Altin>().AltinAc(); // her iki platformdan birinde alt�n olsun diye
            }
               
            SonrakiPlatformPozisyon();

        }
        GameObject olumculPlatform = Instantiate(olumculPlatformPrefab, platformPozisyon, Quaternion.identity);
        olumculPlatform.GetComponent<OlumculPlatform>().Hareket = true;  // olumcullatfrmun hareket eder olmas�n� sa�l�yoruz
        platforms.Add(olumculPlatform);
        SonrakiPlatformPozisyon(); //sonraki platformun pozisyonu hesaplans�n diye  UNUTMA!!!

    }

    void PlatformYerlestir()      //listede ba�tan be� elaman� sona eklemek 
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject temp;
            temp = platforms[i + 5];
            platforms[i + 5] = platforms[i];
            platforms[i] = temp;
            platforms[i + 5].transform.position = platformPozisyon;

            if (platforms[i + 5].gameObject.tag == "Platform")
            {
                platforms[i + 5].GetComponent<Altin>().AltiniKapat();
                float RasgeleAltin = Random.Range(0.0f, 1.0f);               //Alt�nlar�n arasgele olacak �ekilde gelmesini sa�lad�k
                if (RasgeleAltin > 0.5f)
                {
                  platforms[i + 5].GetComponent<Altin>().AltinAc();
                }

            }


            SonrakiPlatformPozisyon();
        }
    }


    //void SonrakiPlatformPozisyon()                         //Bir sonraki platformun yerini haz�r etmek i�in.
    //{
    //    platformPozisyon.y += platformArasiMesafe;
    //    float random = Random.Range(0.0f, 1.0f);
    //    if (random < 0.5f)
    //    {                                                                    //singlenton olan instanceyi burada bir daha kullan�yoruz.
    //        platformPozisyon.x = EkranHesaplayicisi.instance.Genislik / 2;   //Random de�erden d�enen sonu� 0.5 ten k���kse platform sa� taraf�n tam oratas�nda olsun 
    //    }                                                                  
    //    else
    //    {
    //        platformPozisyon.x = -EkranHesaplayicisi.instance.Genislik / 2;
    //    }
    //}
    void SonrakiPlatformPozisyon() //Bir sonraki platformun yerini haz�r etmek i�in.
    {
        platformPozisyon.y += platformArasiMesafe;
        SiraliPozisyon();                              

    }

    void KarmaPozisyon()
    {
        float random = Random.Range(0.0f, 1.0f);
        if (random < 0.5f)
        {
            platformPozisyon.x = EkranHesaplayicisi.instance.Genislik / 2;
        }
        else
        {
            platformPozisyon.x = -EkranHesaplayicisi.instance.Genislik / 2;
        }
    }

    bool yon = true;
    void SiraliPozisyon()  //bir sa� bir sol olacakk �ekilde
    {
        if (yon)
        {
            platformPozisyon.x = EkranHesaplayicisi.instance.Genislik / 2;
            yon = false;
        }
        else
        {
            platformPozisyon.x = -EkranHesaplayicisi.instance.Genislik / 2;
            yon = true;
        }
    }
}
