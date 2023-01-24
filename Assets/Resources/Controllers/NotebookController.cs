using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotebookController : MonoBehaviour
{
    public Image notebookImg;

    [SerializeField]
    private int index;
    [SerializeField]
    private Sprite[] notebookImages;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        GameController.instance.DisableSwitchButton();
    }

    /// <summary>
    /// 切换笔记，下一页
    /// button event
    /// </summary>
    public void NextPage()
    {
        ++index;
        CheckIndexBoundary(index);
        notebookImg.sprite = notebookImages[index];
    }

    /// <summary>
    /// 切换笔记，上一页
    /// button event
    /// </summary>
    public void PrevPage()
    {
        --index;
        CheckIndexBoundary(index);
        notebookImg.sprite = notebookImages[index];
        //Debug.Log(index, notebookImg.sprite);
        //index--;
    }  

    // button event
    public void CloseNoteBookPage()
    {
        index = 0;
        this.gameObject.SetActive(false);
        GameController.instance.EnableCameraMovement();
        GameController.instance.EnableSwitchButton();
    }

    private void CheckIndexBoundary(int index)
    {
        if (index >= notebookImages.Length)
        {
            this.index = 0;
        }
        else if(index < 0)
        {
            this.index = notebookImages.Length - 1;
        }
    }

    /// <summary>
    /// 进入这个函数的初始化操作
    /// </summary>
    private void Init()
    {
        index = 0;
        notebookImages = UIController.instance.notebookImages;
        notebookImg.sprite = notebookImages[0];
        Debug.Log("notebook DisableCameraMovement");
        GameController.instance.DisableCameraMovement();
    }
}
