using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Room2 Puzzle 1 的方法都写在Room2Raycast里了
/// </summary>
public class Room2_Puzzle1 : MonoBehaviour
{
    [Header("五个要弹的琴键")]
    public GameObject[] Room2Puzzle1Object;
    public GameObject puzzle1Object;

    private void OnEnable()
    {
        puzzle1Object.layer = LayerMask.NameToLayer("Interactable");
    }

    private void Start()
    {
    }

    private void OnDisable()
    {
        puzzle1Object.layer = LayerMask.NameToLayer("Default");
    }

    /// <summary>
    /// 等待语音5播放完成后，执行对应的视频播放和解密
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IEnumerator WaitForAudio5(string name)
    {
        var tempClip = AudioController.audioDic[name];
        yield return new WaitUntil(() => tempClip.isPlaying == false);

        Debug.Log($"{name} is finised playing");

        // 播放视频2：开嗓练习。
        VideoController.instance.PlayVideo(0);

        // 与（背景音效2-鲁冰花钢琴曲）。
        AudioController.PlayAudio(AudioName.BGM2);
    }

    public IEnumerator WaitForAudio6(string name)
    {
        var tempClip = AudioController.audioDic[name];
        yield return new WaitUntil(() => tempClip.isPlaying == false);

        // 开启第二个puzzle交互
        PuzzleController.instance.EnableRoom2Puzzle2();
    }

    /// <summary>
    /// 最开始的时候不允许玩家与puzzle1交互
    /// </summary>
    public void OnStartDisableRoom2Puzzle1Object()
    {
        foreach (var obj in Room2Puzzle1Object)
        {
            obj.GetComponent<BoxCollider>().enabled = false;
        }
    }

    /// <summary>
    /// 允许玩家与puzzle1交互
    /// </summary>
    public void EnableRoom2Puzzle1Object()
    {
        foreach (var obj in Room2Puzzle1Object)
        {
            obj.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
