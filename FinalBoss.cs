using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent nav;
    private GameObject Player;
    public float vidaInimigo = 5000;
    public Animator anim;
    public bool wasShot;
    public bool isAtacking;
    public GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
        vidaInimigo = 5000;
        wasShot = false;
        
        anim = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (vidaInimigo <= 0)
        {
            anim.SetInteger("transition", 3);
            nav.enabled = false;
            StartCoroutine("Destruir");
        }
        else
        {
            if (!isAtacking)
            {
                if (Vector3.Distance(Player.transform.position, transform.position) <= 20 || wasShot)
                {
                    nav.destination = Player.transform.position;
                    anim.SetInteger("transition", 1);
                    portal.GetComponent<MeshRenderer>().enabled = true;

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
        Destroy(gameObject);
        Destroy(portal);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isAtacking = true;
            StartCoroutine("Atacar");
            collision.gameObject.GetComponent<Player>().hpPlayer -= 100f*collision.gameObject.GetComponent<Player>().coefDano;
            
        }
    }



    IEnumerator Atacar()
    {
        
        anim.SetInteger("transition", 2);
        yield return new WaitForSeconds(3.0f);
        isAtacking = false;

    }
}
