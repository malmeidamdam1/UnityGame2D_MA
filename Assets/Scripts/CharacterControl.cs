using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private Rigidbody2D personaje;
    private bool mirandoDrecha;
    private Animator animator;

    public float velocidad;
    public float fuerzaSalto;
    public int saltosMaximos;
    public int saltosRestantes;

    private BoxCollider2D boxCollider;
    public LayerMask capaSuelo;
    public AudioClip sonidoSalto; //Asignamos en editor a su prefab

    // Start is called before the first frame update
    void Start()
    {
        personaje = GetComponent<Rigidbody2D>();
        velocidad = 5;
        fuerzaSalto = 10;
        mirandoDrecha = true;
        animator = GetComponent<Animator>();

        boxCollider = GetComponent<BoxCollider2D>();
        capaSuelo = LayerMask.GetMask("Suelo");

        saltosMaximos = 1;
        saltosRestantes = saltosMaximos;
    }

    // Update is called once per frame
    void Update()
    {
        ProcesarMovimiento();
        procesarSalto();
    }

    //Evitar doble salto con arrayCast
    bool estaEnSuelo() 
    {
       Vector2 cajaComprobacion = new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y);

       RaycastHit2D rycastHit = Physics2D.BoxCast(boxCollider.bounds.center, cajaComprobacion, 0f, Vector2.down, 0.2f, capaSuelo);
        return rycastHit.collider != null;
    }

    void procesarSalto() {

        if (estaEnSuelo()) 
        {
            saltosRestantes = saltosMaximos; 
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && saltosRestantes > 0)
        {
            saltosRestantes--;
            personaje.velocity = new Vector2(personaje.velocity.x, 0f);
            personaje.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            AudioManager.Instance.ReproducirSondo(sonidoSalto);
        }
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
