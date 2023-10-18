using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    public GameObject soundSystem;

    public float bgmVolume;
    public float sfxVolume;
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public List<AudioClip> bgmClip;

    /// <summary>
    ///  1 : 시작 소리
    ///  2 : 끝나는 소리
    ///  3 : 게임 지는 소리
    ///  4 : 게임 이기는 소리
    ///  5 : 플레이어 죽는 소리
    ///  6 : 버튼 클릭 소리
    /// </summary>
    public List<AudioClip> sfxClip;

    public Slider bgm_Slider;
    public Slider sfx_Slider;

    public enum bgmClips
    {
        Bgm_01
    }

    public enum sfxClips
    {
        GameStart,
        GameEnd,
        GameLose,
        GameWin,
        PlayerDie,
        Select
    }

    private void Awake() 
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }

        soundSystem = GameObject.Find("Manager/Canvas");
        bgm_Slider = GameObject.Find("Manager/Canvas/Sound/BGMSl").GetComponent<Slider>();
        sfx_Slider = GameObject.Find("Manager/Canvas/Sound/SFXSl").GetComponent<Slider>();

        PlayMusic(bgmClips.Bgm_01);

        PlaySFX(sfxClips.GameStart);

        soundSystem.SetActive(false);
    }

    public void SoundTest()
    {
        PlaySFX(sfxClips.Select);
    }

    public void Hide()
    {
        PlaySFX(sfxClips.Select);
        soundSystem.SetActive(false);
    }

    private void Update() 
    {
        bgmSource.volume = bgm_Slider.value;
        sfxSource.volume = sfx_Slider.value;
    }

    public void PlayMusic(bgmClips clip)
    {
        bgmSource.loop = true;
        bgmSource.clip = bgmClip[(int)clip];
        bgmSource.volume = bgmVolume;
        bgmSource.enabled = true;
        bgmSource.Play();
    }

    public void PlaySFX(sfxClips clip)
    {
        sfxSource.loop = false;
        sfxSource.clip = sfxClip[(int)clip];
        sfxSource.volume = sfxVolume;
        sfxSource.enabled = true;
        sfxSource.Play();
    }
}