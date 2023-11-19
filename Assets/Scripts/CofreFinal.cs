using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CofreFinal : MonoBehaviour
{
    private Animator playerAnimator;
    public AudioClip sonidoAbrir; //Asignamos en editor 
    void Start()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AbrirCofre();
            MostrarMensajeFinJuego();
            AudioManager.Instance.ReproducirSondo(sonidoAbrir);
        }
    }

    void AbrirCofre()
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("AbrirCofre");
        }
    }

    void MostrarMensajeFinJuego()
    {
        Invoke("CargarEscenaFinal", 2f);
    }

    void CargarEscenaFinal()
    {
        //Muestra la escena donde esta el mensaje de WIN
        SceneManager.LoadScene(3);
    }
}
