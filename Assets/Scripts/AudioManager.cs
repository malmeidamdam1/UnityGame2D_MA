using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))] //Crea un audisource automaticamente

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public static AudioManager Instance { get; private set; } //Acceder otras clases

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Más de un AudioManager en escena");
        }
        
    }



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ReproducirSondo(AudioClip audio) //clip fragmento a reproducir
    {
        audioSource.PlayOneShot(audio);
    }
}
