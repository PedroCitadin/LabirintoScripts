using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArmaController : MonoBehaviour
{

    public GameObject efeitoTiro, efeitoSange;
    public Transform pivotTiro;
    public float danoArma = 5;
    public Camera cam;
    Transform mira;
    private Animator anim;
    public Transform skinArma;
    public int Municao = 200;
    // Start is called before the first frame update
    void Start()
    {
        
        mira = GetComponentInChildren<Canvas>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        AtivaDesativaTiros();
    }

    void AtivaDesativaTiros()
    {


        if (Municao > 0) { 
        if (Input.GetMouseButtonDown(0)) {
            Municao--;
            RaycastHit hit = new RaycastHit();
            Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 150f);
            
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == 3)
                {
                    hit.collider.gameObject.GetComponent<Zumbi>().vidaInimigo -= danoArma;
                    hit.collider.gameObject.GetComponent<Zumbi>().wasShot = true;
                    Instantiate(efeitoSange, hit.point, hit.collider.gameObject.transform.rotation, hit.collider.gameObject.transform);
                }else
                if (hit.collider.gameObject.layer == 6)
                {
                    hit.collider.gameObject.GetComponent<Mutante>().vidaInimigo -= danoArma;
                    hit.collider.gameObject.GetComponent<Mutante>().wasShot = true;
                    Instantiate(efeitoSange, hit.point, hit.collider.gameObject.transform.rotation, hit.collider.gameObject.transform);
                }
                else
                if (hit.collider.gameObject.layer == 7)
                {
                    hit.collider.gameObject.GetComponent<FinalBoss>().vidaInimigo -= danoArma;
                    hit.collider.gameObject.GetComponent<FinalBoss>().wasShot = true;
                    Instantiate(efeitoSange, hit.point, hit.collider.gameObject.transform.rotation, hit.collider.gameObject.transform);
                }
                else if (hit.collider.gameObject.tag == "Labirinto" || hit.collider.gameObject.tag == "Ground")
                {

                    Instantiate(efeitoTiro, hit.point, hit.collider.gameObject.transform.rotation, hit.collider.gameObject.transform);
                }
            }
            
}
        }
    }


    
}
