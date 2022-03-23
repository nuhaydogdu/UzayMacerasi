using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamEkran : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); //ekrandaki g�r�nt�m�z�n boyutuna m�dahale etmek i�in
        Vector2 tempScale = transform.localScale;

        float spritGenislik = spriteRenderer.size.x;
        float EkranYukseklik = Camera.main.orthographicSize * 2.0f;
        float EkranGenislik = EkranYukseklik /Screen.height * Screen.width; //spritimizin cal��t�r�lacag� makinedeki ekran geni�lik oran�
        tempScale.x = EkranGenislik / spritGenislik;
        transform.localScale = tempScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
