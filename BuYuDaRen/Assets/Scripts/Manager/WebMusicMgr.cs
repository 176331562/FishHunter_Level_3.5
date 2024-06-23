using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebMusicMgr : MonoBehaviour
{
    private static WebMusicMgr instance;
    public static WebMusicMgr Instance => instance;

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
            audioSource.clip = loadClip[audioClipName];

            audioSource.Play();
        }
        else
        {
            AudioClip webClip = AssetBundleMgr.Instance.LoadAsset<AudioClip>("sound", audioClipName);
            //ResourceRequest rq = Resources.LoadAsync<AudioClip>("Sound/" + audioClipName);

            audioSource.clip = webClip;

            loadClip.Add(audioClipName, webClip);

            audioSource.Play();
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
