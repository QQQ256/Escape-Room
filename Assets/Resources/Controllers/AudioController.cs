using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// 音频控制器，播放、暂停和存储音频
/// </summary>
public class AudioController : MonoBehaviour
{
    /// <summary>
    /// 单个音频的信息
    /// </summary>
    [Serializable]
    public class Sound
    {
        [Header("音频")]
        public AudioClip clip;

        [Header("音频分组")]
        public AudioMixerGroup outputGroup;

        [Header("音量")]
        [Range(0,1)]
        public float volume = 1;

        [Header("是否开局播放")]
        public bool playOnAwake;

        [Header("是否循环播放")]
        public bool loop;
    }

    public List<Sound> sounds;

    /// <summary>
    /// key - name, val - AudioSource组件
    /// </summary>
    public static Dictionary<string, AudioSource> audioDic;

    private static AudioController instance;

    private void Awake()
    {
        instance = this;
        audioDic = new Dictionary<string, AudioSource>();
        InitDictionary();
    }

    private void Start()
    {
    }

    /// <summary>
    /// 播放音频
    /// </summary>
    /// <param name="clipName">音频名称</param>
    /// <param name="isWait">音频是否需要等待后才能播放</param>
    public static void PlayAudio(string clipName, bool isWait = false)
    {
        if (!audioDic.ContainsKey(clipName))
        {
            Debug.LogWarning($"{clipName}音频不存在");
            return;
        }

        if (isWait)
        {
            if (!audioDic[clipName].isPlaying)
            {
                audioDic[clipName].Play();
            }
        }
        else
        {
            audioDic[clipName].Play();
        }
    }

    /// <summary>
    /// 停止音频播放
    /// </summary>
    /// <param name="clipName">音频名称</param>
    public static void StopAudio(string clipName)
    {
        if (!audioDic.ContainsKey(clipName))
        {
            Debug.LogWarning($"{clipName}音频不存在");
            return;
        }
        audioDic[clipName].Stop();
    }


    private void InitDictionary()
    {
        foreach (var sound in sounds)
        {
            GameObject obj = new GameObject(sound.clip.name);
            obj.transform.SetParent(transform);

            AudioSource source = obj.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.playOnAwake = sound.playOnAwake;
            source.loop = sound.loop;
            source.volume = sound.volume;
            source.outputAudioMixerGroup = sound.outputGroup;

            if (sound.playOnAwake)
            {
                source.Play();
            }

            audioDic.Add(sound.clip.name, source);
        }

    }
}
