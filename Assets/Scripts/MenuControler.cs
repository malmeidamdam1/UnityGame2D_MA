using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
    public void Jugar() //Asigna en editor desde button list
    {
        //Carga Escena
        SceneManager.LoadScene(1);
    }
    public void Salir() 
    {
        //Cierra 
        Debug.Log("Gracias por jugar");//Mensaje solo para ver que si esta ejecutandose esto
        Application.Quit();
    }

}
