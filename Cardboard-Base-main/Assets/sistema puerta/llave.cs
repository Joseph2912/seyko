using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class llave : MonoBehaviour  
{
    public puertaScript doorToOpen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            doorToOpen.desbloqueo = true;
        }

        Destroy(gameObject);
    }
}
