using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room1_Puzzle3 : MonoBehaviour
{
    public GameObject puzzle3Object;
    public Camera puzzle3Camera;

    public GameObject nextScene;
    private void OnEnable()
    {
        puzzle3Object.layer = LayerMask.NameToLayer("Interactable");
    }

    private void OnDisable()
    {
        puzzle3Object.layer = LayerMask.NameToLayer("Default");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRoom1Puzzle3Game()
    {
        //puzzle3Object.layer = LayerMask.NameToLayer("Interactable");
        //Debug.Log("puzzle3 DisableCameraMovement!");
        GameController.instance.DisableCameraMovement();
        Debug.Log("Start game 3!");

        puzzle3Camera.GetComponent<Camera>().enabled = true;


        Debug.Log("end game 3");
    }

    public IEnumerator WaitForAudio4(string name)
    {
        var tempClip = AudioController.audioDic[name];
        yield return new WaitUntil(() => tempClip.isPlaying == false);

        Debug.Log($"{name} is finised playing");

        // 打开机关1
        PuzzleController.instance.EnableRoom1Puzzle3();

        PuzzleController.instance.EnableRoom1Puzzle3();
    }

    public void EnableNextScene()
    {
        nextScene.gameObject.SetActive(true);
    }
}
