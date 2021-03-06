using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class follow : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawnZone;
    [SerializeField] private GameObject enemybody;
    [SerializeField] private GameObject enemyhead;
    [SerializeField] private Material quads;
    [SerializeField] private GameObject lights;
    [SerializeField] private float vaina = 0;
    [SerializeField] private GameObject playerhead;
    [SerializeField] private GameObject cameraplayer;
  
    private float enemywidth = 0.5f;
    private float raymaxdistance = 200f;
    public bool spawnState = false;
    private float rayspawndistance;
    public float angle = 45f;
    private int cant = 200 ;
    private Vector3 EnemyOut = new Vector3(0, -10, 0);
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform moveToPosition;
    [SerializeField] float lerptime;
    private Color enemycolor;
    private Color Noenemycolor;
    public bool playerlooking;
    private Color lightcolor;
    private float lightchek;
    public bool playerdead;
    public float timer = 0;
    private int i = 0;
    void Start()
    {
        playerdead = false;
       // spawnState = false;
        //lerptime = 0.02f;
        Noenemycolor = new Color(0.650f, 0.650f, 0.650f);
        enemycolor = new Color(0.811f, 0.249f, 0.249f);
        quads.color = Noenemycolor;
        //quads.color = new Color(0.811f, 0.249f, 0.249f);
        transform.position = EnemyOut;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        playerhead.transform.position = new Vector3(player.transform.position.x, playerhead.transform.position.y, player.transform.position.z);
        player.transform.rotation = Quaternion.Euler(0, playerhead.transform.rotation.eulerAngles.y, 0);
        player.transform.rotation = Quaternion.Euler(0, cameraplayer.transform.rotation.eulerAngles.y, 0);

        if (playerdead == false)
        {
            if (spawnState == true)
            {
                if (timer < 15)
                {
                    timer += Time.deltaTime;
                }
                if (Input.GetKeyDown(KeyCode.F) || timer > 15)
                {
                    timer = 0;
                    enemyOut();
                    
                }
                lightcolor = lights.GetComponent<Light>().color;
                lightchek = lightcolor.r + lightcolor.g + lightcolor.b;
                if (lightchek < 0.3f)
                {
                    playerdead = true;
                }
                quads.color = Color.Lerp(quads.color, enemycolor, lerptime);
                EnemyLook();
            }
            else
            {
                if(timer < 6)
                {
                   timer += Time.deltaTime;
                }
                print(timer);
                if(timer > 5 && rayspawndistance < -10f)
                {
                    //print(timer);
                    transform.position = spawnZone.transform.position;
                    spawnState = true;
                    //timer = 0;
                }
                if (Input.GetKeyDown(KeyCode.M) && rayspawndistance < -15f)
                {
                    transform.position = spawnZone.transform.position;
                    
                    spawnState = true;
                }

                quads.color = Color.Lerp(quads.color, Noenemycolor, lerptime);
                Spawncheck();
            }
        }
        else
        {
            
            Killplayer();
        }
    }
    private void Killplayer()
    {
        if(i == 0)
        {
            timer = 0;
            i = 1;
        }
       
        playerdead = true;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<Rigidbody>().Sleep();
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        gameObject.GetComponent<Collider>().enabled = false;

        transform.parent = player.transform.transform;

        transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        playerhead.transform.rotation = Quaternion.Euler(-7.5f, playerhead.transform.eulerAngles.y, 0);
        timer += Time.deltaTime;
        print(timer);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0f, transform.localPosition.y, 0.6f), 40 * Time.deltaTime);
        if(timer > 7)
        {
            restart();
        }
    }
    private void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void PlayerLook()
    {
        int nhit = 0;
        RaycastHit hit;
        for (int i = 0; i <= cant; i++)
        {
            float rayanglespacing = (angle * 2 / cant);
            Debug.DrawRay(player.transform.position + new Vector3(0, 2, 0), (Quaternion.AngleAxis(angle - (rayanglespacing * i), player.transform.up) * player.transform.forward) * raymaxdistance, Color.blue);
            bool enemyisHit = Physics.Raycast(origin: player.transform.position + new Vector3(0, 2, 0), direction: (Quaternion.AngleAxis(angle - (rayanglespacing * i), player.transform.up) * player.transform.forward), out hit, raymaxdistance);
            if (enemyisHit)
            {
                if(hit.transform.gameObject == gameObject)
                {
                    nhit = +1;
                    break;
                }
                
            }
           
        }
        if (nhit != 0)
        {
            playerlooking = true;
            gameObject.GetComponent<Rigidbody>().Sleep();
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            enemystoped();   
        }
        else
        {
            playerlooking = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            gameObject.GetComponent<Rigidbody>().WakeUp();
            Enemymoving();
        }
        
    }
    private void EnemyLook()
    {

        int nhit = 0;
        enemyhead.transform.forward = player.transform.position - enemyhead.transform.position; //mirar jugador
        RaycastHit hit;
       
        for (int i = 0; i < 3; i++)
        {
            
            float rayspacing = (enemywidth / 2) * -1;
            bool playerisHit = Physics.Raycast(origin: transform.position + new Vector3(rayspacing + (rayspacing * -i), 2, 0), direction: (Quaternion.AngleAxis(0, enemyhead.transform.up) * enemyhead.transform.forward), out hit, raymaxdistance);
            if (playerisHit)
            {
                if (hit.transform.gameObject == player )
                {
                    if (hit.distance < 2)
                    {
                        Killplayer();
                    }
                    nhit += 1;
                }
               
            }
        }
        if (nhit != 0)
        {
            
            PlayerLook();
        }
        else
        {
            enemyOut();
        }
        
    }
    private void enemyOut()
    {
        gameObject.GetComponent<Rigidbody>().Sleep();
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        transform.position = EnemyOut;
        enemystoped();
        spawnState = false;
    }
    private void Enemymoving()
    {
        transform.forward = player.transform.position - transform.position; //mirar jugador
        navMeshAgent.destination = moveToPosition.position;
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemyspeed * Time.deltaTime);
        for (int i = 0; i < 3; i++)
        {
            float rayspacing = (enemywidth / 2) * -1;
            Debug.DrawRay(transform.position + new Vector3(rayspacing + (rayspacing * -i), 2, 0), (Quaternion.AngleAxis(0, enemyhead.transform.up) * enemyhead.transform.forward) * raymaxdistance, Color.green);
        }
    }
    private void enemystoped()
    {
        for (int i = 0; i < 3; i++)
        {
            float rayspacing = (enemywidth / 2) * -1;
            Debug.DrawRay(transform.position + new Vector3(rayspacing + (rayspacing * -i), 2, 0), (Quaternion.AngleAxis(0, enemyhead.transform.up) * enemyhead.transform.forward) * raymaxdistance, Color.red);
        }
    }
    private void Spawncheck()
    {
        RaycastHit hit;
        Debug.DrawRay(player.transform.position + new Vector3(0,2.67f, 0), (Quaternion.AngleAxis(180, player.transform.up) * player.transform.forward) * raymaxdistance, Color.cyan);
        bool wallisHit = Physics.Raycast(origin: player.transform.position + new Vector3(0, 2.67f, 0), direction: (Quaternion.AngleAxis(180, player.transform.up) * player.transform.forward), out hit, raymaxdistance);
        if (wallisHit)
        {
            float hitdistance = (hit.distance*-1);
            rayspawndistance = hitdistance;
            
            spawnZone.transform.parent = player.transform.transform;

            //spawnZone.transform.localPosition = new Vector3(spawnZone.transform.localPosition.x, spawnZone.transform.localPosition.y, hitdistance / 1.1f);

            spawnZone.transform.localPosition = new Vector3(spawnZone.transform.localPosition.x, spawnZone.transform.localPosition.y, hitdistance/2.6f);
            
        }
        
    }

}
