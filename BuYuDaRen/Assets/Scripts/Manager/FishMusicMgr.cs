using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMusicMgr : MonoBehaviour
{
    private static FishMusicMgr instance;
    public static FishMusicMgr Instance => instance;

    private AudioSource audioSource;


    private Dictionary<string, AudioClip> loadClip = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        audioSource = this.GetComponent<AudioSource>();
    }



    public void PlayerAudio(string audioClipName)
    {
        if (loadClip.ContainsKey(audioClipName))
        {
            if(!audioSource.isPlaying)
            {
                audioSource.clip = loadClip[audioClipName];

                audioSource.Play();
            }
            

        }
        else
        {           
            if (!audioSource.isPlaying)
            {

                AudioClip fishClip = AssetBundleMgr.Instance.LoadAsset<AudioClip>("sound", audioClipName);
                //ResourceRequest rq = Resources.LoadAsync<AudioClip>("Sound/" + audioClipName);

                audioSource.clip = fishClip;

                loadClip.Add(audioClipName, fishClip);

                audioSource.Play();
            }           
        }
    }

    public void SetIsOpen(bool b)
    {
        audioSource.mute = !b;
    }

    public void SetSoundValue(float value)
    {
        audioSource.volume = value;
    }

    private void OnEnable()
    {
        SetIsOpen(GameDataMgr.Instane.musicData.isOpenSound);

        SetSoundValue(GameDataMgr.Instane.musicData.soundValue);
    }
}
