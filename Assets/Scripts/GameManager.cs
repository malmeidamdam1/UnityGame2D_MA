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

    public void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Debug.Log("Más de 1 gameManager en escena");
        }
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

        if (vidas == 0) 
        {
            SceneManager.LoadScene(0);
        }
    }
 
}