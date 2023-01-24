using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2_Puzzle2 : MonoBehaviour
{
    public GameObject puzzle2Object;
    public Light lampLight;
    private void OnEnable()
    {
        puzzle2Object.layer = LayerMask.NameToLayer("Interactable");
        lampLight.GetComponent<Light>().enabled = true;
    }

    private void OnDisable()
    {
        puzzle2Object.layer = LayerMask.NameToLayer("Default");
    }


    /// <summary>
    /// ”Ô“Ù7ºÃ–¯≤•∑≈
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IEnumerator WaitForAudio7(string name)
    {
        var tempClip = AudioController.audioDic[name];
        yield return new WaitUntil(() => tempClip.isPlaying == false);
        Debug.Log("Audio 7 is Finished");

        //
        PuzzleController.instance.EnableRoom2Puzzle3();
    }
}
