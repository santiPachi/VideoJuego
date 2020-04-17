using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class CrashEffect : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource m_AudioSource;
    private bool PlayingAudio;
    void Start()
    {   PlayingAudio = false;
        m_AudioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
   
    public void PlayAudio()
    {
      
        m_AudioSource.Play();
        PlayingAudio = true;
    }


    public void StopAudio()
    {
        m_AudioSource.Stop();
        PlayingAudio = false;
    }

     void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Untagged")){
            PlayAudio();   
        }
            
    }


}
