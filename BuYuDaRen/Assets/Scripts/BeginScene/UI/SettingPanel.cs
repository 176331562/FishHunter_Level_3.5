using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

        });


        musicSlider.onValueChanged.AddListener((v) =>
        {
            BKMusicMgr.Instance.SetMusicValue(v);
        });

        soundSlider.onValueChanged.AddListener((v) =>
        {

        });

        btnClose.onClick.AddListener(() =>
        {
            SaveValue();

            UIManager.Instance.CloseThisPanel<SettingPanel>(true, () =>
            {
                UIManager.Instance.ShowThisPanel<BeginPanel>();
            });
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

        MusicData musicData = GameDataMgr.Instane.musicData;

        musicToggle.isOn = musicData.isOpenMusic;
        soundToggle.isOn = musicData.isOpenSound;

        musicSlider.value = musicData.musicValue;
        soundSlider.value = musicData.soundValue;
    }
}
