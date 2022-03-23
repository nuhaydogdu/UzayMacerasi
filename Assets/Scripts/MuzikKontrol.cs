using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzikKontrol : MonoBehaviour
{
    public static MuzikKontrol instance;   //singelton olarak kullanýyoruz tüm proje boyunca bundan bir tane olacak

    AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        Singleton();
        audioSource = GetComponent<AudioSource>(); // audioSource un kimin referansý olduðunu belirtiyoruz
    }

    void Singleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance); // bu method sayesinde sahneler arasý geçiþte instance yi yok etme demiþ olduk.
        }
    }

    public void MuzikCal(bool play)
    {
        if (play)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
