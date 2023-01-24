using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// 控制视频播放的控制器
/// 不要移动视频
/// </summary>
public class VideoController : MonoBehaviour
{
    [Header("所有视频")]
    public List<VideoClip> videoPlayers = new List<VideoClip>();

    [Header("播放视频的Object")]
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
    /// 播放视频
    /// 需要指定index播放
    /// </summary>
    /// <param name="index">videoPlayers下标</param>
    public void PlayVideo(int index)
    {
        tvVideoPlayer.clip = videoPlayers[index];
        tvVideoPlayer.Play();

        DelegateVideoStatus(index);
    }

    /// <summary>
    /// 基于index来委托视频播放结束后要做的事情
    /// </summary>
    /// <param name="index">视频下标</param>
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
    /// 视频1播放结束干点啥子？
    /// </summary>
    /// <param name="vp"></param>
    public void WaitForVideo1(VideoPlayer vp)
    {
        Debug.Log($"{vp.name} video is finished");
        //AudioController.StopAudio(AudioName.BGM2);

        // 开启简谱
        PuzzleController.instance.puzzle1.isFinished = false;

        // 音符墙亮起简谱数字板块
        PuzzleController.instance.EnableRoom2Puzzle1();

        // 打开所有碰撞体
        PuzzleController.instance.EnableRoom2Puzzle1Object();
    }

    /// <summary>
    /// 视频2播放结束，全场播放（语音6-林天星）
    /// </summary>
    /// <param name="vp"></param>
    public void WaitForVideo2(VideoPlayer vp)
    {
        Debug.Log($"{vp.clip.name} video is finished");
        AudioController.PlayAudio(AudioName.Audio6);
        AudioController.PlayAudio(AudioName.BGM2);
        // 每个音频都需要等待播放结束才能进入下一个阶段
        StartCoroutine(PuzzleController.instance.gameObject.GetComponentInChildren<Room2_Puzzle1>().WaitForAudio6(AudioName.Audio6));
    }
}
