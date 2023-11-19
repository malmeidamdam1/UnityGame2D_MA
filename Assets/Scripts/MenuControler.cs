using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
    public void Jugar() //Asigna en editor desde button list
    {
        SceneManager.LoadScene(1);
    }
    public void Salir() 
    {
        Debug.Log("Gracias por jugar");
        Application.Quit();
    }
}
