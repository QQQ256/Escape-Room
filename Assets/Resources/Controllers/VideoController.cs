using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// ������Ƶ���ŵĿ�����
/// ��Ҫ�ƶ���Ƶ
/// </summary>
public class VideoController : MonoBehaviour
{
    [Header("������Ƶ")]
    public List<VideoClip> videoPlayers = new List<VideoClip>();

    [Header("������Ƶ��Object")]
    public GameObject tv;

    private VideoPlayer tvVideoPlayer;
    
    public static VideoController instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        tvVideoPlayer = tv.GetComponent<VideoPlayer>();
        //PlayVideo(1);
        //StartCoroutine(CloseTV());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ������Ƶ
    /// ��Ҫָ��index����
    /// </summary>
    /// <param name="index">videoPlayers�±�</param>
    public void PlayVideo(int index)
    {
        tvVideoPlayer.clip = videoPlayers[index];
        tvVideoPlayer.Play();

        DelegateVideoStatus(index);
    }

    /// <summary>
    /// ����index��ί����Ƶ���Ž�����Ҫ��������
    /// </summary>
    /// <param name="index">��Ƶ�±�</param>
    public void DelegateVideoStatus(int index)
    {
        if (index == 0)
        {
            tvVideoPlayer.loopPointReached += WaitForVideo1;
        }
        else if (index == 1)
        {
            AudioController.StopAudio(AudioName.BGM2);
            tvVideoPlayer.loopPointReached += WaitForVideo2;
        }
    }

    /// <summary>
    /// ��Ƶ1���Ž����ɵ�ɶ�ӣ�
    /// </summary>
    /// <param name="vp"></param>
    public void WaitForVideo1(VideoPlayer vp)
    {
        Debug.Log($"{vp.name} video is finished");
        //AudioController.StopAudio(AudioName.BGM2);

        // ��������
        PuzzleController.instance.puzzle1.isFinished = false;

        // ����ǽ����������ְ��
        PuzzleController.instance.EnableRoom2Puzzle1();

        // ��������ײ��
        PuzzleController.instance.EnableRoom2Puzzle1Object();
    }

    /// <summary>
    /// ��Ƶ2���Ž�����ȫ�����ţ�����6-�����ǣ�
    /// </summary>
    /// <param name="vp"></param>
    public void WaitForVideo2(VideoPlayer vp)
    {
        Debug.Log($"{vp.clip.name} video is finished");
        AudioController.PlayAudio(AudioName.Audio6);
        AudioController.PlayAudio(AudioName.BGM2);
        // ÿ����Ƶ����Ҫ�ȴ����Ž������ܽ�����һ���׶�
        StartCoroutine(PuzzleController.instance.gameObject.GetComponentInChildren<Room2_Puzzle1>().WaitForAudio6(AudioName.Audio6));
    }
}
