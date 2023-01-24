using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Room2 Puzzle 1 �ķ�����д��Room2Raycast����
/// </summary>
public class Room2_Puzzle1 : MonoBehaviour
{
    [Header("���Ҫ�����ټ�")]
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
    /// �ȴ�����5������ɺ�ִ�ж�Ӧ����Ƶ���źͽ���
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IEnumerator WaitForAudio5(string name)
    {
        var tempClip = AudioController.audioDic[name];
        yield return new WaitUntil(() => tempClip.isPlaying == false);

        Debug.Log($"{name} is finised playing");

        // ������Ƶ2����ɤ��ϰ��
        VideoController.instance.PlayVideo(0);

        // �루������Ч2-³��������������
        AudioController.PlayAudio(AudioName.BGM2);
    }

    public IEnumerator WaitForAudio6(string name)
    {
        var tempClip = AudioController.audioDic[name];
        yield return new WaitUntil(() => tempClip.isPlaying == false);

        // �����ڶ���puzzle����
        PuzzleController.instance.EnableRoom2Puzzle2();
    }

    /// <summary>
    /// �ʼ��ʱ�����������puzzle1����
    /// </summary>
    public void OnStartDisableRoom2Puzzle1Object()
    {
        foreach (var obj in Room2Puzzle1Object)
        {
            obj.GetComponent<BoxCollider>().enabled = false;
        }
    }

    /// <summary>
    /// ���������puzzle1����
    /// </summary>
    public void EnableRoom2Puzzle1Object()
    {
        foreach (var obj in Room2Puzzle1Object)
        {
            obj.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
