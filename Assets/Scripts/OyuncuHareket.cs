using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyuncuHareket : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animator;   //animator kontrollerini saðlayabilmek için

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

    Joystick joystick;                                //yön çubuðu için

    JoystickButon joystickButon;                      //zýplama butonu için 

    bool zipliyor;

    // Start is called before the first frame update
    void Start()
    {
        joystickButon = FindObjectOfType<JoystickButon>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        joystick = FindObjectOfType<Joystick>(); //sahnede joystick tipinde bileþeni olan objeyi bul ve onu buradaki deðiþken ata
    }                                            //(sahnedeki joystiðin bize saðladýðý özellikleri kullanbileli diye)

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR                               //platform dependent compilation
               KlavyeKontrol();               //eðer bu kodun çalýþtýðý yer unýty editörse -klavyekontrol(); eðer deðilse (mobilse) - joystickkontrol();
#else
        JoystickKontrol();
#endif

    }

    void KlavyeKontrol()
    {
        float hareketInput = Input.GetAxisRaw("Horizontal"); //yatay doðrultuda hareketi saðlaya bilmemk için
        Vector2 scale = transform.localScale;                   //scale deðerini kullnarak objemizin yönünü deðiþtiriyoruz

        if (hareketInput > 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, hareketInput * hiz, hizlanma * Time.deltaTime);
            animator.SetBool("Walk", true);
            scale.x = 0.3f;                                    // buradaki 0.3 player objemizin boyutundan farklý birþey yazarsak görüntü bozuluyor.
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

        if (Input.GetKeyDown("space"))             // zýplama kontrolleri burada
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
        float hareketInput = joystick.Horizontal;      //yatay doðrultuda hareketi saðlaya bilmemk için
        Vector2 scale = transform.localScale;           //scale deðerini kullnarak objemizin yönünü deðiþtiriyoruz

        if (hareketInput > 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, hareketInput * hiz, hizlanma * Time.deltaTime);
            animator.SetBool("Walk", true);
            scale.x = 0.3f;                   // buradaki 0.3 player objemizin boyutundan farklý birþey yazarsak görüntü bozuluyor.
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

        if (joystickButon.tusaBasildi == true && zipliyor == false) //tek baþýna tusaBasýldý yeterli olmuyor update in içerisinde çaðýrdýðýmýz için her statatteki hareketini kontrol ederek uyguluyoruz
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
            rb2d.AddForce(new Vector2(0, ziplamaGucu), ForceMode2D.Impulse);      // y ekseninde zýplamak için rigidbody2d ye -addforce uyguluyoruz!!!
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