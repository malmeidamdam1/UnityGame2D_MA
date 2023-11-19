using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinMapaControler : MonoBehaviour
{
    // Le doy valor por editor
    public GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Autokill
            gameManager.Morir();
        }
    }

}
