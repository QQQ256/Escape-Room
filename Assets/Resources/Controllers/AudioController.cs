using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// ��Ƶ�����������š���ͣ�ʹ洢��Ƶ
/// </summary>
public class AudioController : MonoBehaviour
{
    /// <summary>
    /// ������Ƶ����Ϣ
    /// </summary>
    [Serializable]
    public class Sound
    {
        [Header("��Ƶ")]
        public AudioClip clip;

        [Header("��Ƶ����")]
        public AudioMixerGroup outputGroup;

        [Header("����")]
        [Range(0,1)]
        public float volume = 1;

        [Header("�Ƿ񿪾ֲ���")]
        public bool playOnAwake;

        [Header("�Ƿ�ѭ������")]
        public bool loop;
    }

    public List<Sound> sounds;

    /// <summary>
    /// key - name, val - AudioSource���
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
    /// ������Ƶ
    /// </summary>
    /// <param name="clipName">��Ƶ����</param>
    /// <param name="isWait">��Ƶ�Ƿ���Ҫ�ȴ�����ܲ���</param>
    public static void PlayAudio(string clipName, bool isWait = false)
    {
        if (!audioDic.ContainsKey(clipName))
        {
            Debug.LogWarning($"{clipName}��Ƶ������");
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
    /// ֹͣ��Ƶ����
    /// </summary>
    /// <param name="clipName">��Ƶ����</param>
    public static void StopAudio(string clipName)
    {
        if (!audioDic.ContainsKey(clipName))
        {
            Debug.LogWarning($"{clipName}��Ƶ������");
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
