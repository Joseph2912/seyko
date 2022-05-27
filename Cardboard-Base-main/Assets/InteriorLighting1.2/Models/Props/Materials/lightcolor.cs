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
        
        if(enemy.GetComponent<follow>().spawnState == true)
        {
            if(playerlooking == true)
            {
                ObjectColor.color = Color.Lerp(ObjectColor.color, lookingcolor, 0.007f);
            }
            else
            {
                ObjectColor.color = Color.Lerp(ObjectColor.color, enemycolor, lerptime);
            }
            
        }
        else
        {
            
            ObjectColor.color = Color.Lerp(ObjectColor.color, Noenemycolor, lerptime);

        }
    }
}
