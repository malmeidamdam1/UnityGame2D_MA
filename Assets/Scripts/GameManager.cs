using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    //Para acceder desde demas scripts
    public static GameManager Instance { get; private set; }

    //Para acceder al valor desde fuera de la clase 
    public int PuntosTotales { get { return puntosTotales; } }

    private int puntosTotales;

    public HUD hud; //Asignamos en editor

    private int vidas = 3;

    private Animator playerAnimator;  

    public AudioClip sonidoMorir; //Asignamos en editor



    public void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Debug.Log("Más de 1 gameManager en escena");
        }
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }


    public void SumarPuntos(int puntosSumar)
    {
        puntosTotales += puntosSumar;
        hud.ActualizarPuntos(puntosTotales);
    }

    public void PerderVida() 
    {
        vidas --;
        hud.DesactivarVida(vidas);

        if (vidas == 0) //Muere que vaya al gameOver
        {
            Morir();
        }
    }

    //Lo hago publico para matarlo directamente desde finMapa.cs
    public void Morir() 
    {
        {
            playerAnimator.SetTrigger("irMorir");
            AudioManager.Instance.ReproducirSondo(sonidoMorir);
        }
        StartCoroutine(EsperarYMostrarGameOver());
    }

    IEnumerator EsperarYMostrarGameOver()
    {
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(2);
    }


}