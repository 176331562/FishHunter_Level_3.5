using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusicMgr : MonoBehaviour
{

    private static BKMusicMgr instance;
    public static BKMusicMgr Instance => instance;


    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;

        audioSource = this.GetComponent<AudioSource>();

        MusicData musicData = GameDataMgr.Instane.musicData;

        SetMusicIsOpen(musicData.isOpenMusic);
        SetMusicValue(musicData.musicValue);
    }


    public void SetMusicIsOpen(bool b)
    {
        audioSource.mute = !b;
    }

    public void SetMusicValue(float value)
    {
        audioSource.volume = value;
    }
}
