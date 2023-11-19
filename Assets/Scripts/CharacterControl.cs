using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private Rigidbody2D personaje;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float velocidad;
    private float fuerzaSalto;
    private float fuerzaEnemigo;
    private int saltosMaximos;
    private int saltosRestantes;
    private bool puedeMoverse = true;
    private bool mirandoDrecha = true;

    private bool enCooldownDash = false;
    private float tiempoCooldownDash = 0.5f;

    private LayerMask capaSuelo;
    public AudioClip sonidoSalto; //Asignamos en editor
    public AudioClip sonidoGolpe; //Asignamos en editor

    void Start()
    {
        personaje = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        capaSuelo = LayerMask.GetMask("Suelo");
        velocidad = 5;
        fuerzaSalto = 10;
        fuerzaEnemigo = 500;
        saltosMaximos = 1;
        saltosRestantes = saltosMaximos;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();
    }

    //Evitar doble salto con arrayCast
    bool EstaEnSuelo() 
    {
       Vector2 cajaComprobacion = new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y);

       RaycastHit2D rycastHit = Physics2D.BoxCast(boxCollider.bounds.center, cajaComprobacion, 0f, Vector2.down, 0.2f, capaSuelo);
        return rycastHit.collider != null;
    }

    void ProcesarSalto() {

        if (EstaEnSuelo()) 
        {
            saltosRestantes = saltosMaximos;
            animator.SetBool("isJumping", false);
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && saltosRestantes > 0)
        {
            animator.SetBool("isJumping", true);
            saltosRestantes--;
            personaje.velocity = new Vector2(personaje.velocity.x, 0f);
            personaje.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            AudioManager.Instance.ReproducirSondo(sonidoSalto);

        }
    }


    void ProcesarMovimiento() 
    {
        //Salir si no se puede mover
        if (puedeMoverse == false) 
        {
            return;
        }

        //Dashear
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Dash();
        }

        //Devuelve negativo si presiona a izquierda/ Positivo si presiona a derecha / 0 si quieto
        float inputMovimiento = Input.GetAxis("Horizontal");
        if (inputMovimiento != 0f)
        {
            animator.SetBool("isWalking", true);
        }
        else 
        {
            animator.SetBool("isWalking", false);
        }

        //Indicar que no esta saltando
        if (EstaEnSuelo()) 
        {
            animator.SetBool("isJumping", false);
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


    void Dash() 
    {
        // Evitar el dash si no se puede mover o tiene cooldown
        if (!puedeMoverse || enCooldownDash)
        {
            return;
        }

        // Desplazar al personaje hacia adelante
        float distanciaDash = 0.5f; // Ajusta la distancia según tus necesidades
        Vector2 nuevaPosicion = transform.position + (mirandoDrecha ? Vector3.right : Vector3.left) * distanciaDash;
        personaje.MovePosition(nuevaPosicion);

        animator.SetBool("isDashing", true);

        StartCoroutine(ResetearDash());
    }

    IEnumerator ResetearDash()
    {
        //Dejamos que haga la animacion antes de detener
        yield return new WaitForSeconds(1); 

        // Detener el dash y activar el cooldown
        animator.SetBool("isDashing", false);
        enCooldownDash = true;


        // Esperar el tiempo de cooldown antes de poder dashear nuevamente y reiniciamos poder hacer dash
        yield return new WaitForSeconds(tiempoCooldownDash);
        enCooldownDash = false;
    }



    public void RecibirGolpe() 
    {
        puedeMoverse = false;

        Vector2 direccionGolpe;

        if (personaje.velocity.x > 0)
        {
            direccionGolpe = new Vector2(-1, 1);
        }
        else 
        {
            direccionGolpe = new Vector2(1, 1);
        }
        personaje.AddForce(direccionGolpe * fuerzaEnemigo);
        AudioManager.Instance.ReproducirSondo(sonidoGolpe);
        StartCoroutine(EsperarYMover());
    }

    public void Quemarse()
    {
        puedeMoverse = false;

        Vector2 direccionAtras;

        if (personaje.velocity.x > 0)
        {
            direccionAtras = new Vector2(-1, 1);
        }
        else
        {
            direccionAtras = new Vector2(1, 1);
        }

        float distanciaAtras = 100;
        personaje.AddForce(direccionAtras * distanciaAtras);
        AudioManager.Instance.ReproducirSondo(sonidoGolpe);
        StartCoroutine(EsperarYMover());
    }


    IEnumerator EsperarYMover()
    {
        //esperar antes de comprobar
        yield return new WaitForSeconds(0.1f);

        while (!EstaEnSuelo()) 
        {
            yield return null;
        }
        puedeMoverse = true;
    }

}
