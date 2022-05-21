using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawnZone;
    private float enemyspeed = 5f;
    private float enemywidth = 0.5f;
    private float raymaxdistance = 200f;
    private bool spawnState = false;
    public float angle = 45f;
    private int cant = 200 ;
    private Vector3 EnemyOut = new Vector3(0, -10, 0);
   
    void Start()
    {
        transform.position = EnemyOut;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (spawnState == true)
        {
            EnemyLook();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                transform.position = spawnZone.transform.position;
                spawnState = true;
            }
          
            Spawncheck();
        }
        
    }
    private void PlayerLook()
    {
        int nhit = 0;
        RaycastHit hit;
        for (int i = 0; i <= cant; i++)
        {
            float rayanglespacing = (angle * 2 / cant);
            Debug.DrawRay(player.transform.position + new Vector3(0, 0, 0), (Quaternion.AngleAxis(angle - (rayanglespacing * i), player.transform.up) * player.transform.forward) * raymaxdistance, Color.blue);
            bool enemyisHit = Physics.Raycast(origin: player.transform.position + new Vector3(0, 0, 0), direction: (Quaternion.AngleAxis(angle - (rayanglespacing * i), player.transform.up) * player.transform.forward), out hit, raymaxdistance);
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
            enemystoped();
        }
        else
        {
            Enemymoving();
        }
    }
    private void EnemyLook()
    {
        int nhit = 0;
        transform.forward = player.transform.position - transform.position; //mirar jugador
        RaycastHit hit;
       
        for (int i = 0; i < 3; i++)
        {
            
            float rayspacing = (enemywidth / 2) * -1;
            bool playerisHit = Physics.Raycast(origin: transform.position + new Vector3(rayspacing + (rayspacing * -i), 0, 0), direction: (Quaternion.AngleAxis(0, transform.up) * transform.forward), out hit, raymaxdistance);
            if (playerisHit)
            {
                if (hit.transform.gameObject == player)
                {
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
            transform.position = EnemyOut;
            enemystoped();
            spawnState = false;
        }
        
    }
    private void Enemymoving()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemyspeed * Time.deltaTime);
        for (int i = 0; i < 3; i++)
        {
            float rayspacing = (enemywidth / 2) * -1;
            Debug.DrawRay(transform.position + new Vector3(rayspacing + (rayspacing * -i), 0, 0), (Quaternion.AngleAxis(0, transform.up) * transform.forward) * raymaxdistance, Color.green);
        }
    }
    private void enemystoped()
    {
        for (int i = 0; i < 3; i++)
        {
            float rayspacing = (enemywidth / 2) * -1;
            Debug.DrawRay(transform.position + new Vector3(rayspacing + (rayspacing * -i), 0, 0), (Quaternion.AngleAxis(0, transform.up) * transform.forward) * raymaxdistance, Color.red);
        }
    }
    private void Spawncheck()
    {
        RaycastHit hit;
        Debug.DrawRay(player.transform.position + new Vector3(0, 0, 0), (Quaternion.AngleAxis(180, player.transform.up) * player.transform.forward) * raymaxdistance, Color.cyan);
        bool wallisHit = Physics.Raycast(origin: player.transform.position + new Vector3(0, 0, 0), direction: (Quaternion.AngleAxis(180, player.transform.up) * player.transform.forward), out hit, raymaxdistance);
        if (wallisHit)
        {
            float hitdistance = (hit.distance*-1)+2;
            print(hitdistance);
         
            spawnZone.transform.parent = player.transform.transform;
            spawnZone.transform.localPosition = new Vector3(spawnZone.transform.localPosition.x, spawnZone.transform.localPosition.y, hitdistance);
            
        }
        
    }

}