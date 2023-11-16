using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI puntos;

   /*
    void Start()
    {
        puntos = GetComponent<TextMeshProUGUI>();
    }
   */
    void Update()
    {
        puntos.text = gameManager.PuntosTotales.ToString();
        Debug.Log(gameManager.PuntosTotales.ToString());
    }


}
