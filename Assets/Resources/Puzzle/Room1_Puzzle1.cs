using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room1_Puzzle1 : MonoBehaviour
{
    public GameObject puzzle1Object;
    public Image startQuestionImage;
    public GameObject question1;

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

    public void StartRoom1Puzzle1()
    {
        puzzle1Object.layer = LayerMask.NameToLayer("Interactable");
        GameController.instance.DisableCameraMovement();
        startQuestionImage.gameObject.SetActive(true);
    }

    public IEnumerator WaitForAudio2(string name)
    {
        var tempClip = AudioController.audioDic[name];
        yield return new WaitUntil(() => tempClip.isPlaying == false);

        Debug.Log($"{name} is finised playing");

        // 打开机关1
        PuzzleController.instance.EnableRoom1Puzzle1();

    }
}
