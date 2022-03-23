using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyuncuHareket : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animator;   //animator kontrollerini sa�layabilmek i�in

    Vector2 velocity;

    [SerializeField]
    float hiz = default;

    [SerializeField]
    float hizlanma = default;

    [SerializeField]
    float yavaslama = default;

    [SerializeField]
    float ziplamaGucu = default;

    [SerializeField]
    int ziplamaLimiti = 3;

    int ziplamaSayisi;

    Joystick joystick;                                //y�n �ubu�u i�in

    JoystickButon joystickButon;                      //z�plama butonu i�in 

    bool zipliyor;

    // Start is called before the first frame update
    void Start()
    {
        joystickButon = FindObjectOfType<JoystickButon>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        joystick = FindObjectOfType<Joystick>(); //sahnede joystick tipinde bile�eni olan objeyi bul ve onu buradaki de�i�ken ata
    }                                            //(sahnedeki joysti�in bize sa�lad��� �zellikleri kullanbileli diye)

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR                               //platform dependent compilation
               KlavyeKontrol();               //e�er bu kodun �al��t��� yer un�ty edit�rse -klavyekontrol(); e�er de�ilse (mobilse) - joystickkontrol();
#else
        JoystickKontrol();
#endif

    }

    void KlavyeKontrol()
    {
        float hareketInput = Input.GetAxisRaw("Horizontal"); //yatay do�rultuda hareketi sa�laya bilmemk i�in
        Vector2 scale = transform.localScale;                   //scale de�erini kullnarak objemizin y�n�n� de�i�tiriyoruz

        if (hareketInput > 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, hareketInput * hiz, hizlanma * Time.deltaTime);
            animator.SetBool("Walk", true);
            scale.x = 0.3f;                                    // buradaki 0.3 player objemizin boyutundan farkl� bir�ey yazarsak g�r�nt� bozuluyor.
        }
        else if (hareketInput < 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, hareketInput * hiz, hizlanma * Time.deltaTime);
            animator.SetBool("Walk", true);
            scale.x = -0.3f;
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, yavaslama * Time.deltaTime);
            animator.SetBool("Walk", false);
        }

        transform.localScale = scale;
        transform.Translate(velocity * Time.deltaTime);

        if (Input.GetKeyDown("space"))             // z�plama kontrolleri burada
        {
            ZiplamayiBaslat();
        }

        if (Input.GetKeyUp("space"))
        {
            ZiplamayiDurdur();
        }
    }

    void JoystickKontrol()
    {
        float hareketInput = joystick.Horizontal;      //yatay do�rultuda hareketi sa�laya bilmemk i�in
        Vector2 scale = transform.localScale;           //scale de�erini kullnarak objemizin y�n�n� de�i�tiriyoruz

        if (hareketInput > 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, hareketInput * hiz, hizlanma * Time.deltaTime);
            animator.SetBool("Walk", true);
            scale.x = 0.3f;                   // buradaki 0.3 player objemizin boyutundan farkl� bir�ey yazarsak g�r�nt� bozuluyor.
        }
        else if (hareketInput < 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, hareketInput * hiz, hizlanma * Time.deltaTime);
            animator.SetBool("Walk", true);
            scale.x = -0.3f;
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, yavaslama * Time.deltaTime);
            animator.SetBool("Walk", false);
        }

        transform.localScale = scale;
        transform.Translate(velocity * Time.deltaTime);

        if (joystickButon.tusaBasildi == true && zipliyor == false) //tek ba��na tusaBas�ld� yeterli olmuyor update in i�erisinde �a��rd���m�z i�in her statatteki hareketini kontrol ederek uyguluyoruz
        {
            zipliyor = true;
            ZiplamayiBaslat();
        }

        if (joystickButon.tusaBasildi == false && zipliyor == true)
        {
            zipliyor = false;
            ZiplamayiDurdur();
        }
    }

    void ZiplamayiBaslat()
    {
        if (ziplamaSayisi < ziplamaLimiti)
        {
            FindObjectOfType<SesKontrol>().ZiplamaSes();
            rb2d.AddForce(new Vector2(0, ziplamaGucu), ForceMode2D.Impulse);      // y ekseninde z�plamak i�in rigidbody2d ye -addforce uyguluyoruz!!!
            animator.SetBool("Jump", true);
            FindObjectOfType<SliderKontrol>().SliderDeger(ziplamaLimiti, ziplamaSayisi);
        }
    }

    void ZiplamayiDurdur()
    {
        animator.SetBool("Jump", false);
        ziplamaSayisi++;
        FindObjectOfType<SliderKontrol>().SliderDeger(ziplamaLimiti, ziplamaSayisi);
    }

    public void ZiplamayiSifirla()
    {
        ziplamaSayisi = 0;
        FindObjectOfType<SliderKontrol>().SliderDeger(ziplamaLimiti, ziplamaSayisi);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Olum")
        {
            FindObjectOfType<OyunKontrol>().OyunuBitir(); 
        }
    }

    public void OyunBitti()
    {
        Destroy(gameObject);
    }


}