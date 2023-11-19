using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuegoControler : MonoBehaviour
{
    public float volverDa�ar = 3;
    public bool quema = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!quema) return;

        quema = false;

        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.PerderVida();

            collision.gameObject.GetComponent<CharacterControl>().Quemarse();

            Invoke("ActivarQuemado", volverDa�ar);
        }
    }
    void ActivarQuemado()
    {
        quema = true;
    }


}
