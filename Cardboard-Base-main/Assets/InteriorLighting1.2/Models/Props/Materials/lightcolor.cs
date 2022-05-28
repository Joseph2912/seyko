using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightcolor : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float lerptime;
    private Color enemycolor;
    private Color Noenemycolor;
    private Color lookingcolor;
    public Light ObjectColor;
    private bool playerlooking;
    private bool playerdead; 
    void Start()
    {
        
        ObjectColor = gameObject.GetComponent<Light>();
        Noenemycolor = new Color(1f, 1f, 1f,1f);
        enemycolor = new Color(1f, 0.149f, 0.149f,1f);
        lookingcolor = new Color(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        playerlooking = enemy.GetComponent<follow>().playerlooking;
        playerdead = enemy.GetComponent<follow>().playerdead;

        if (enemy.GetComponent<follow>().spawnState == true)
        {
            if(playerlooking == true && playerdead== false)
            {
                ObjectColor.color = Color.Lerp(ObjectColor.color, lookingcolor, 0.007f);
            }
            else
            {
                if(playerdead == false)
                {
                    ObjectColor.color = Color.Lerp(ObjectColor.color, enemycolor, lerptime);
                }  
            }
            if (playerdead == true)
            {
                ObjectColor.color = Color.Lerp(ObjectColor.color, enemycolor, 0.03f);
            }
            
        }
        else
        {
            
            ObjectColor.color = Color.Lerp(ObjectColor.color, Noenemycolor, lerptime);

        }
    }
}
