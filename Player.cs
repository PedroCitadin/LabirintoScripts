using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public float velocidade = 5;
    public float velocidadeCorrida = 9;
    public KeyCode teclaDeCorrida = KeyCode.LeftShift;
    public Animator anim;
    public Rigidbody rb;
    private float velocidadeAtual;
    public float hpPlayer = 250;
    public ArmaController ac;
    public float coefDano = 1;
    public bool healing = false;
    public GameController gc;
    public GameObject miniMap;
    public float recoveryRate = 0.01f; // Taxa de recuperação de vida por segundo
    public GameObject HUDDano;
    public GameObject HUDEscudo;
    public GameObject HUDRegen;
    
    private CharacterController controller;
    private float recoveryTimer;
    // Start is called before the first frame update
    void Awake()
    {

        controller = GetComponent<CharacterController>();
        velocidadeAtual = velocidade;
        recoveryTimer = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var inputX = Input.GetAxis("Horizontal");
        var inputZ = Input.GetAxis("Vertical");

        if (inputX != 0 || inputZ != 0)
        {
            Vector3 moveDirection = transform.TransformDirection(new Vector3(inputX, 0, inputZ));
            controller.SimpleMove(moveDirection * velocidadeAtual);

            anim.SetInteger("transition", 1);
            if (Input.GetKey(teclaDeCorrida))
            {
                anim.SetInteger("transition", 2);
                velocidadeAtual = velocidadeCorrida;
            }
            else
            {
                anim.SetInteger("transition", 1);
                velocidadeAtual = velocidade;
            }
        }
        else
        {
            anim.SetInteger("transition", 0);
            velocidadeAtual = velocidade;
        }

        if (healing)
        {
            heal();
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Dano1")
        {
            Destroy(collider.gameObject);
            ac.danoArma *= 10;
            HUDDano.SetActive(true);
        }
        if (collider.gameObject.tag == "Dano2")
        {
            Destroy(collider.gameObject);
            ac.danoArma *= 10;
        }
        if (collider.gameObject.tag == "Escudo")
        {
            Destroy(collider.gameObject);
            coefDano = coefDano / 4;
            HUDEscudo.SetActive(true);
        }
        if (collider.gameObject.tag == "Regenera")
        {
            Destroy(collider.gameObject);
            healing = true;
            HUDRegen.SetActive(true);
        }
        if (collider.gameObject.tag == "CheckPoint")
        {
            Destroy(collider.gameObject);
            gc.spawnPoint.position = transform.position;
        }
        if (collider.gameObject.tag == "Mapa")
        {
            Destroy(collider.gameObject);
            miniMap.SetActive(true);
        }
        if (collider.gameObject.tag == "Contador")
        {
            velocidade = 10;
            velocidadeCorrida = 17;
            Destroy(collider.gameObject);
            
        }
        if (collider.gameObject.tag == "EndGame")
        {
            SceneManager.LoadScene("YouWin");
        }
    }

    void heal()
    {
        if (hpPlayer < 250)
        {
            // Incrementa o temporizador de recuperação
            recoveryTimer += Time.deltaTime;

            // Verifica se é hora de recuperar a vida
            if (recoveryTimer >= 1f / recoveryRate)
            {
                // Recupera uma porcentagem da vida máxima

                hpPlayer += recoveryRate;

                // Reinicia o temporizador
                recoveryTimer = 0f;
            }
        }
        if (hpPlayer > 250)
        {
            hpPlayer = 250;
        }
    }




}
