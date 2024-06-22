using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SettingPanel : BasePanel
{
    private Toggle musicToggle;
    private Toggle soundToggle;

    private Slider musicSlider;
    private Slider soundSlider;

    private Button btnClose;

    protected override void Awake()
    {
        base.Awake();

        musicToggle = this.transform.Find("SettingBK/MusicToggle").GetComponent<Toggle>();
        soundToggle = this.transform.Find("SettingBK/SoundToggle").GetComponent<Toggle>();

        musicSlider = this.transform.Find("SettingBK/MusicSlider").GetComponent<Slider>();
        soundSlider = this.transform.Find("SettingBK/SoundSlider").GetComponent<Slider>();

        btnClose = this.transform.Find("SettingBK/CloseBtn").GetComponent<Button>();
    }

    public override void Init()
    {
        musicToggle.onValueChanged.AddListener((b) =>
        {
            BKMusicMgr.Instance.SetMusicIsOpen(b);
        });

        soundToggle.onValueChanged.AddListener((b) =>
        {
            if(SceneManager.GetActiveScene().name == "GameScene")
            {
                FireMusicMgr.Instance.SetIsOpen(b);
                WebMusicMgr.Instance.SetIsOpen(b);
                FishMusicMgr.Instance.SetIsOpen(b);
            }
        });


        musicSlider.onValueChanged.AddListener((v) =>
        {
            BKMusicMgr.Instance.SetMusicValue(v);
        });

        soundSlider.onValueChanged.AddListener((v) =>
        {
            FireMusicMgr.Instance.SetSoundValue(v);
            FishMusicMgr.Instance.SetSoundValue(v);
            WebMusicMgr.Instance.SetSoundValue(v);
        });

        btnClose.onClick.AddListener(() =>
        {

            if(SceneManager.GetActiveScene().name != "GameScene")
            {
                SaveValue();

                UIManager.Instance.CloseThisPanel<SettingPanel>(true, () =>
                {
                    UIManager.Instance.ShowThisPanel<BeginPanel>();
                });
            }
            else
            {
                SaveValue();

                UIManager.Instance.CloseThisPanel<SettingPanel>(true);
            }
            
        });
    }

    private void SaveValue()
    {
        MusicData musicData = GameDataMgr.Instane.musicData;
        musicData.isOpenMusic = musicToggle.isOn;
        musicData.isOpenSound = soundToggle.isOn;

        musicData.musicValue = musicSlider.value;
        musicData.soundValue = soundSlider.value;

        GameDataMgr.Instane.musicData = musicData;

        GameDataMgr.Instane.SaveMusicData(musicData);
    }

    public override void ShowThisPanel()
    {
        base.ShowThisPanel();

        //如果第一次登录是没有musicData的就得去创建
        if(GameDataMgr.Instane.musicData == null)
        {
            MusicData musicDataDefault = new MusicData();

            GameDataMgr.Instane.SaveMusicData(musicDataDefault);
        }

        if(GameDataMgr.Instane.musicData != null)
        {
            MusicData musicData = GameDataMgr.Instane.musicData;

            musicToggle.isOn = musicData.isOpenMusic;
            soundToggle.isOn = musicData.isOpenSound;

            musicSlider.value = musicData.musicValue;
            soundSlider.value = musicData.soundValue;
        }       
    }
}
