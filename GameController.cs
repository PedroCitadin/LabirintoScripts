using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Player Player;
    public float vidas = 3;
    public Transform spawnPoint;
    public GameObject groupRoot;
    public Text scoreHP;
    public Text scoreVidas;
    public int contadorKills;
    public Text scoreKills;
    public GameObject FinalBoss;
    public GameObject Mutante;
    public bool isDemo;
    public GameObject HUDKills;
    public GameObject AC;
    public Text ScoreMunicao;
    // Start is called before the first frame update
    void Start()
    {
        isDemo = false;
        contadorKills = 0;
        FinalBoss = GameObject.FindWithTag("FinalBoss");
        Mutante = GameObject.FindWithTag("Mutante");
        AC = GameObject.FindWithTag("Arma");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        verificaVidaPlayer();
        verificaVida();
        atualizaHUD();

        if (isDemo)
        {
            if (Input.GetKey(KeyCode.H))
            {
                Player.transform.position = new Vector3(666.1f, 0f, 450f);
            }
            if (Input.GetKey(KeyCode.J))
            {
                Player.transform.position = new Vector3(603.92f, 0f, 99.68f);
            }
            if (Input.GetKey(KeyCode.K))
            {
                Player.transform.position = new Vector3(410.8f, 0f, 178.42f);
            }
            if (Input.GetKey(KeyCode.L))
            {
                Player.transform.position = new Vector3(340.6189f, 0f, 373f);
            }

        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            
            isDemo = !isDemo;
            HUDKills.SetActive(isDemo);
        }


    }


   
   

  

    void verificaVidaPlayer()
    {
        if (Player.hpPlayer <= 0)
        {
            vidas--;
            Player.transform.position = spawnPoint.position;
            Player.transform.rotation = spawnPoint.rotation;
            Player.hpPlayer = 250;
            var components = groupRoot.GetComponentsInChildren<Zumbi>();
            foreach (var component in components)
            {
                // Modifique a propriedade do componente conforme necessário
                component.wasShot = false; ;
            }
            FinalBoss.GetComponent<FinalBoss>().wasShot = false;
            Mutante.GetComponent<Mutante>().wasShot = false;

        }
    }

    void verificaVida()
    {
        
        if (vidas <= 0)
        {
            Destroy(Player);
            SceneManager.LoadScene("GameOver");

        }
    }

    void atualizaHUD()
    {
        scoreHP.text = Player.hpPlayer+"/250";
        scoreVidas.text = vidas.ToString();
        scoreKills.text = contadorKills.ToString();
        ScoreMunicao.text = AC.GetComponent<ArmaController>().Municao.ToString();
    }
    
}
