using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// animation event
/// 播放结束后进入谜题3
/// </summary>
public class Flower : MonoBehaviour
{
    public void FinishPuzzle2()
    {
        PuzzleController.instance.FinishPuzzle2();
    }
}
