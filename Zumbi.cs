using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zumbi : MonoBehaviour
{


    private UnityEngine.AI.NavMeshAgent nav;
    private GameObject Player;
    private GameObject GC;
    public float vidaInimigo = 100;
    public Animator anim;
    public bool wasShot;
    public GameObject FB;
    public GameObject AC;
    // Start is called before the first frame update
    void Start()
    {
        wasShot = false;
        anim = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Player = GameObject.FindWithTag("Player");
        GC = GameObject.FindWithTag("GameController");
        FB = GameObject.FindWithTag("FinalBoss");
        AC = GameObject.FindWithTag("Arma");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (vidaInimigo <=0)
        {
            anim.SetInteger("transition", 2);
            nav.enabled = false;
            
            StartCoroutine("Destruir");
        }
        else
        {
            if (Vector3.Distance(Player.transform.position, transform.position)<=15 || wasShot) { 
            nav.destination = Player.transform.position;
              anim.SetInteger("transition", 1);

            }
            else
            {
                anim.SetInteger("transition", 0);
            }
         }
          

    }

    IEnumerator Destruir()
    {
        
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
        AC.GetComponent<ArmaController>().Municao += 30;
        GC.GetComponent<GameController>().contadorKills++;
        FB.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
        FB.GetComponent<FinalBoss>().vidaInimigo += 10;

    }

   void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag =="Player")
        {
            collision.gameObject.GetComponent<Player>().hpPlayer -=0.5f * collision.gameObject.GetComponent<Player>().coefDano; ;
        }
    }
}
