using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("单张图片")]
    public Sprite[] onePaperImages; // 存所有要展示的单个图纸
    [Header("笔记本图片")]
    public Sprite[] notebookImages;
    [Header("笔记本UI")]
    public GameObject notebookPanelUI;
    [Header("单张图UI")]
    public GameObject paperPanelUI;
    [Header("密码UI")]
    public GameObject passwordUI;

    public static UIController instance { get; private set; }

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region public method

    /// <summary>
    /// 弹出单张纸页面
    /// </summary>
    public void ShowPaperPanel(RaycastHit hitInfo)
    {
        string name = hitInfo.collider.name;
        if (GameController.instance.obj_img_dicts.ContainsKey(name))
        {
            ShowPaperPanel();
            UpdatePaperPanelImg(name);
            GameController.instance.DisableCameraMovement();
            Debug.Log("PaperPanel DisableCameraMovement");
        }
    }

    /// <summary>
    /// 笔记本页面
    /// </summary>
    public void ShowNotebookPanel()
    {
        UIController.instance.notebookPanelUI.gameObject.SetActive(true);
        GameController.instance.DisableCameraMovement();
    }

    public void ShowPaperPanel()
    {
        paperPanelUI.gameObject.SetActive(true);
    }

    public void UpdatePaperPanelImg(string name)
    {
        paperPanelUI.transform.GetChild(0).GetComponent<Image>().sprite = GameController.instance.obj_img_dicts[name];
        Debug.Log(GameController.instance.obj_img_dicts[name] + "!!!!");
    }
    #endregion

    #region private method

    #endregion
}
