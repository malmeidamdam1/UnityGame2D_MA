using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoManager : MonoBehaviour
{
    public float cooldownAtk;
    private bool puedeAtacar = true;
    private SpriteRenderer sprite; 

    private void Start()
    {
        if (cooldownAtk == 0) 
        {
            cooldownAtk = 3;

        }
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            //Si no puede atacar salimos
            if (!puedeAtacar) return;

            //Desactivar para que deje de pegar
            puedeAtacar = false;

            //Hacerlo titilar
            Color titilar = sprite.color;
            titilar.a = 0.5f;
            sprite.color = titilar;
             
            GameManager.Instance.PerderVida();

            collision.gameObject.GetComponent<CharacterControl>().RecibirGolpe();

            Invoke("ReactivarAtk", cooldownAtk);
        }
    }

    void ReactivarAtk()
    {
        puedeAtacar = true;
        Color vueltaOriginal = sprite.color;
        vueltaOriginal.a = 1f;
        sprite.color = vueltaOriginal;
    }

}
