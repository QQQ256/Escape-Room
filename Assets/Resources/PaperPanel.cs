using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 很多功能需要弹出“一张纸”，这个脚本将所有要弹出的一张纸写在一起
/// 用一个panel中的img来展示和替换
/// </summary>
public class PaperPanel : MonoBehaviour
{
    public Image paperImg; 
    // Start is called before the first frame update
    void Start()
    {
        GameController.instance.DisableCameraMovement();
    }

    public void ClosePaperPanel()
    {
        GameController.instance.EnableCameraMovement();
        this.gameObject.SetActive(false);
    }
}
