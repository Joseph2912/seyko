using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private bool m_IsWalking;
    [SerializeField] private float m_RunSpeed;
    [SerializeField] private AudioClip[] m_FootstepSounds;

    private AudioSource m_AudioSource;


    float speed;
    void Start()
    {

        m_AudioSource = GetComponent<AudioSource>();
        speed = 5.0f;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            ProgressStepCycle();

        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-1 * Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        
    }

    private void ProgressStepCycle()
    {
        

        PlayFootStepAudio();
    }


    private void PlayFootStepAudio()
    {
        
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, m_FootstepSounds.Length);
        m_AudioSource.clip = m_FootstepSounds[n];
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        m_FootstepSounds[n] = m_FootstepSounds[0];
        m_FootstepSounds[0] = m_AudioSource.clip;
        
    }
}
