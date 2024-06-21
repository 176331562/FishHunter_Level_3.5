using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMusicMgr : MonoBehaviour
{
    
    private static FireMusicMgr instance;
    public static FireMusicMgr Instance => instance;

    private AudioSource audioSource;


    private Dictionary<string, AudioClip> loadClip = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        audioSource = this.GetComponent<AudioSource>();
    }



    public void PlayerAudio(string audioClipName)
    {
        if(loadClip.ContainsKey(audioClipName))
        {
            audioSource.clip = loadClip[audioClipName];

            audioSource.Play();
        }
        else
        {
            ResourceRequest rq = Resources.LoadAsync<AudioClip>("Sound/" + audioClipName);

            audioSource.clip = rq.asset as AudioClip;

            loadClip.Add(audioClipName, rq.asset as AudioClip);

            audioSource.Play();
        }        
    }


  
}
