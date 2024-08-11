using System.Collections;
using System.Collections.Generic;
using FrameWork.Utils;
using UnityEngine;

public class AudioManager : PersistentUnitySingleton<AudioManager>
{
    [SerializeField] AudioSource sFXPlayer;
    [SerializeField] AudioSource bgmPlayer;
    
    [SerializeField] float minPitch = 0.9f;

    [SerializeField] float maxPitch = 1.1f;

    /// <summary>
    /// 音を出す
    /// </summary>
    /// <param name="audioData">音データ</param>
    public void PlaySFX(AudioData audioData)
    {
        sFXPlayer.PlayOneShot(audioData.audioClip, audioData.volueme);
    }

    /// <summary>
    /// Pitchをランダムに変更して音を出す
    /// </summary>
    /// <param name="audioData">音データ</param>
    public void PlayRandomSFX(AudioData audioData)
    {
        sFXPlayer.pitch = Random.Range(minPitch, maxPitch);
        PlaySFX(audioData);
    }

    /// <summary>
    /// いくつかの音源をランダムに流す
    /// </summary>
    /// <param name="audioData">音データ配列</param>
    public void PlayRandomSFX(AudioData[] audioData)
    {
        PlayRandomSFX(audioData[Random.Range(0, audioData.Length)]);
    }
    
    /// <summary>
    /// BGMの変更
    /// </summary>
    /// <param name="audioData">音データ</param>
    public void ChangeBGM(AudioData audioData)
    {
        bgmPlayer.clip = audioData.audioClip;
        bgmPlayer.volume = audioData.volueme;
        bgmPlayer.Play();
    }
}

/// <summary>
/// AudioClipとvoluemeをまとめるクラス
/// </summary>
[System.Serializable]
public class AudioData
{
    /// <summary>
    /// 音源
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// 音量
    /// </summary>
    public float volueme;
}
