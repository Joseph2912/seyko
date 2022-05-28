using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puertaScript : MonoBehaviour
{

    public Transform door;
    public float speed = 1f;
    public Transform openTransform;
    Vector3 targetPosition;
    float time;
    public Transform closeTransform;
    public bool desbloqueo = true;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = closeTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(desbloqueo && door.position != targetPosition)
        {
            door.transform.position = Vector3.Lerp(door.transform.position, targetPosition, time);
            time += Time.deltaTime * speed;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            targetPosition = openTransform.position;
            time = 0;
        }
    }


   void OnTriggerExit(Collider other)
{
    if (other.tag == "Player")
    {
        targetPosition = closeTransform.position;
            time = 0;
    }
}

}
