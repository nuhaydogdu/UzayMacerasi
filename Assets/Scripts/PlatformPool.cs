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

    List<GameObject> platforms = new List<GameObject>(); //oyunda kullanacağımız tüm plarformları tutan listimiz. 


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
        if (platforms[platforms.Count - 1].transform.position.y <           //Listemizin içerisindeki son elamanımızın y değerini kameranin ye değeriyle kıyaslıyoruz
            Camera.main.transform.position.y + EkranHesaplayicisi.instance.Yukseklik)
        {
            PlatformYerlestir();
        }
    }

    void PlatformUret()
    {
        platformPozisyon = new Vector2(0, 0);
        playerPozisyon = new Vector2(0, 0.5f);

        GameObject player = Instantiate(playerPrefab, playerPozisyon, Quaternion.identity);          //sahnede var etme işlemi
        GameObject ilkPlatform = Instantiate(platformPrefab, platformPozisyon, Quaternion.identity);
        player.transform.parent = ilkPlatform.transform;  // aralarında parent child ilişkisi kurduk (player ve ilk platformu birbirine bağladık gibi)
        platforms.Add(ilkPlatform);
        SonrakiPlatformPozisyon();
        ilkPlatform.GetComponent<Platform>().Hareket = false;

        for (int i = 0; i < 8; i++)
        {
            GameObject platform = Instantiate(platformPrefab, platformPozisyon, Quaternion.identity);
            platforms.Add(platform);
            platform.GetComponent<Platform>().Hareket = true; //polatformun hareket kontrolünü sağlıyoruz (hareket platform scriptinde tanımlı).

            if (i % 2 == 0)
            {
                platform.GetComponent<Altin>().AltinAc(); // her iki platformdan birinde altın olsun diye
            }
               
            SonrakiPlatformPozisyon();

        }
        GameObject olumculPlatform = Instantiate(olumculPlatformPrefab, platformPozisyon, Quaternion.identity);
        olumculPlatform.GetComponent<OlumculPlatform>().Hareket = true;  // olumcullatfrmun hareket eder olmasını sağlıyoruz
        platforms.Add(olumculPlatform);
        SonrakiPlatformPozisyon(); //sonraki platformun pozisyonu hesaplansın diye  UNUTMA!!!

    }

    void PlatformYerlestir()      //listede baştan beş elamanı sona eklemek 
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
                float RasgeleAltin = Random.Range(0.0f, 1.0f);               //Altınların arasgele olacak şekilde gelmesini sağladık
                if (RasgeleAltin > 0.5f)
                {
                  platforms[i + 5].GetComponent<Altin>().AltinAc();
                }

            }


            SonrakiPlatformPozisyon();
        }
    }


    //void SonrakiPlatformPozisyon()                         //Bir sonraki platformun yerini hazır etmek için.
    //{
    //    platformPozisyon.y += platformArasiMesafe;
    //    float random = Random.Range(0.0f, 1.0f);
    //    if (random < 0.5f)
    //    {                                                                    //singlenton olan instanceyi burada bir daha kullanıyoruz.
    //        platformPozisyon.x = EkranHesaplayicisi.instance.Genislik / 2;   //Random değerden döenen sonuç 0.5 ten küçükse platform sağ tarafın tam oratasında olsun 
    //    }                                                                  
    //    else
    //    {
    //        platformPozisyon.x = -EkranHesaplayicisi.instance.Genislik / 2;
    //    }
    //}
    void SonrakiPlatformPozisyon() //Bir sonraki platformun yerini hazır etmek için.
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
    void SiraliPozisyon()  //bir sağ bir sol olacakk şekilde
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
