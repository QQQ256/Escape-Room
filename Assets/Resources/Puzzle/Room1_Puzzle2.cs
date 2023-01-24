using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1_Puzzle2 : MonoBehaviour
{
    public GameObject puzzle2Object;
    public Canvas puzzle2Canvas;
    public Camera puzzleCamera;
    private void OnEnable()
    {
        puzzle2Object.layer = LayerMask.NameToLayer("Interactable");
    }

    private void Start()
    {
    }

    public void StartRoom1Puzzle2()
    {
        puzzle2Object.layer = LayerMask.NameToLayer("Interactable");
        GameController.instance.DisableCameraMovement();
        EnableCanvas();
        Debug.Log("Start room1 puzzle 2!");

        puzzle2Canvas.enabled = true;
    }

    /// <summary>
    /// button event
    /// </summary>
    public void ReturnToMainScene()
    {
        GameController.instance.EnableSwitchButton();
        GameController.instance.EnableCurrentCamera();
        GameController.instance.EnableCameraMovement();
        DisableCanvas();
    }

    /// <summary>
    /// button event
    /// 完成华容道后的弹窗
    /// </summary>
    public void FinishedPuzzle2()
    {
        Debug.Log("finished puzzle 2");
        GameController.instance.EnableCurrentCamera();
        DisableCanvas();
        PuzzleController.instance.DisableRoom1Puzzle2();
        PuzzleController.instance.EnableRoom1Puzzle3();
        PuzzleController.instance.StartRoom1Puzzle3();
    }

    public IEnumerator WaitForAudio3(string name)
    {
        var tempClip = AudioController.audioDic[name];
        yield return new WaitUntil(() => tempClip.isPlaying == false);

        Debug.Log($"{name} is finised playing");

        // 打开机关1
        PuzzleController.instance.EnableRoom1Puzzle2();
        Debug.Log("开启puzzle2");
    }

    private void EnableCanvas()
    {
        puzzle2Canvas.GetComponent<Canvas>().enabled = true;
    }

    private void DisableCanvas()
    {
        puzzle2Canvas.GetComponent<Canvas>().enabled = false;

    }

    public void EnablePuzzleCamera()
    {
        puzzleCamera.GetComponent<Camera>().enabled = true;
    }

    public void DisablePuzzleCamera()
    {
        puzzleCamera.GetComponent<Camera>().enabled = false;
    }
}
