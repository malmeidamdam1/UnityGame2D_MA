using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private Rigidbody2D personaje;
    public float velocidad;
    private bool mirandoDrecha;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        personaje = GetComponent<Rigidbody2D>();
        velocidad = 5;
        mirandoDrecha = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcesarMovimiento();
    }

    void ProcesarMovimiento() 
    {
        //Devuelve negativo si presiona a izquierda/ Positivo si presiona a derecha / 0 si quieto
        float inputMovimiento = Input.GetAxis("Horizontal");


        if (inputMovimiento != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else 
        {
            animator.SetBool("isWalking", false);
        }


        personaje.velocity = new Vector2(inputMovimiento * velocidad, personaje.velocity.y);

        GestionarOrientacion(inputMovimiento);



    }

    void GestionarOrientacion(float inputMovimiento)
    {

        if(mirandoDrecha==true && inputMovimiento < 0 || mirandoDrecha == false && inputMovimiento > 0 )
        {
            mirandoDrecha = !mirandoDrecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

}
