using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI puntos;
    public GameObject[] vidas; //Asigna en editor
   
    public void ActualizarPuntos(int puntosTotales) 
    {
        puntos.text = puntosTotales.ToString();
    }

    public void DesactivarVida(int indice) 
    {
        if (indice <= 2)
        {
            vidas[indice].SetActive(false);
        }
    }  
}
