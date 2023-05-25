using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutante : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent nav;
    private GameObject Player;
    private GameObject GC;
    public float vidaInimigo = 1000f;
    public Animator anim;
    public bool wasShot;
    public bool isAtacking;
    public GameObject loot;
    public GameObject AC;
    // Start is called before the first frame update
    void Start()
    {
        vidaInimigo = 1000f;
        wasShot = false;
        anim = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Player = GameObject.FindWithTag("Player");
        GC = GameObject.FindWithTag("GameController");
        AC = GameObject.FindWithTag("Arma");
    }

    // Update is called once per frame
    void Update()
    {

        if (vidaInimigo <= 0)
        {
            anim.SetInteger("transition", 3);
            nav.enabled = false;
            loot.SetActive(true);
            
            StartCoroutine("Destruir");
        }
        else
        {
            if (!isAtacking)
            {
                if (Vector3.Distance(Player.transform.position, transform.position) <= 35 || wasShot)
                {
                    nav.destination = Player.transform.position;
                    anim.SetInteger("transition", 1);

                }
                else
                {
                    anim.SetInteger("transition", 0);
                }
            }
        }


    }

    IEnumerator Destruir()
    {
        yield return new WaitForSeconds(3.0f);
        AC.GetComponent<ArmaController>().Municao += 400;
        Destroy(gameObject);
        GC.GetComponent<GameController>().contadorKills++;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isAtacking = true;
            StartCoroutine("Atacar");
            collision.gameObject.GetComponent<Player>().hpPlayer -= 50f * collision.gameObject.GetComponent<Player>().coefDano; ;
            
        }
    }

    

    IEnumerator Atacar()
    {
        Debug.Log("aqui");
        anim.SetInteger("transition", 2);
        yield return new WaitForSeconds(3.0f);
        isAtacking = false;
       
    }
}
