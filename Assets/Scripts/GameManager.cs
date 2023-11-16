using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    //Propiedad acceder al valor desde fuera de la clase (en HUD)
    public int PuntosTotales { get { return puntosTotales; } }

    private int puntosTotales;

    public void SumarPuntos(int puntosSumar)
    {
        puntosTotales += puntosSumar;
       // Debug.Log(puntosTotales);

    }

    // public void changeScene() {
    //   SceneManager.LoadScene(escenaNueva);
    //}

}