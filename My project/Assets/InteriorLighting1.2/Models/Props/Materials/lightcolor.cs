using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightcolor : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float lerptime;
    private Color enemycolor;
    private Color Noenemycolor;
    private Light ObjectColor;
    void Start()
    {
        ObjectColor = gameObject.GetComponent<Light>();
        Noenemycolor = new Color(1f, 1f, 1f,1f);
        enemycolor = new Color(1f, 0.149f, 0.149f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(enemy.GetComponent<follow>().spawnState == true)
        {
            
            ObjectColor.color = Color.Lerp(ObjectColor.color, enemycolor,lerptime);
        }
        else
        {
            
            ObjectColor.color = Color.Lerp(ObjectColor.color, Noenemycolor, lerptime);

        }
    }
}
