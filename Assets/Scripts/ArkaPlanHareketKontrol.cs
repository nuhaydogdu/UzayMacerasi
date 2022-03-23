using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArkaPlanHareketKontrol : MonoBehaviour
{
    float arkaPlanKonum;
    float mesafe = 10.24f; //ortan noktalar�na g�re i�lem yap�yoruz

    // Start is called before the first frame update
    void Start()
    {
        arkaPlanKonum = transform.position.y;
        FindObjectOfType<Gezegenler>().GezegenYerlestir(arkaPlanKonum); // �htiyac�m�z olna y de�eri arka plan�n Konum de�eri ile ayn� pldu�u i�i.
    }

    // Update is called once per frame
    void Update()
    {
        if (arkaPlanKonum + mesafe < Camera.main.transform.position.y)
        {
            ArkaPlanYerlestir();
        }
    }
    void ArkaPlanYerlestir()
    {
        arkaPlanKonum += (mesafe * 2);
        FindObjectOfType<Gezegenler>().GezegenYerlestir(arkaPlanKonum); 
        Vector2 yeniPozisyon = new Vector2(0, arkaPlanKonum);
        transform.position = yeniPozisyon;
    
    }
}
