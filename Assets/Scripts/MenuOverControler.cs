using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuOverControler : MonoBehaviour
{
    //Asignan en editor desde button list
    public void Rejugar() 
    {
        //Recarga nivelPrincipal
        SceneManager.LoadScene(1);
    }
    public void Menu()
    {
        //Carga menuInicial
        SceneManager.LoadScene(0);
    }
}
