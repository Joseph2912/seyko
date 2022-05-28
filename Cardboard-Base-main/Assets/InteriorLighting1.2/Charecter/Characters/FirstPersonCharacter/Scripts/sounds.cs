using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] m_enemysounds;    // an array of footstep sounds that will be randomly selected from.
    [SerializeField] private AudioSource s_audioplayer;
    [SerializeField] private AudioSource d_audioplayer;
    private bool spawnstate;
    private bool playerdead;
    private bool isplaying;

    // Start is called before the first frame update
    void Start()
    {
        isplaying = false;
    }

    // Update is called once per frame
    void Update()
    {

        spawnstate = GameObject.Find("bicho").GetComponent<follow>().spawnState;
        playerdead = GameObject.Find("bicho").GetComponent<follow>().playerdead;
        
;       if(spawnstate == true && isplaying == false)
        {
            s_audioplayer.clip = m_enemysounds[0];
            s_audioplayer.PlayOneShot(s_audioplayer.clip);
            isplaying = true;
        }
        else
        {
            if(spawnstate == false)
            {
                isplaying = false;
                s_audioplayer.Stop();
            }
            
        }
        if(playerdead == true && d_audioplayer.isPlaying == false)
        {
            s_audioplayer.Stop();
            d_audioplayer.clip = m_enemysounds[1];
            d_audioplayer.PlayOneShot(d_audioplayer.clip);

        }
    }
}
