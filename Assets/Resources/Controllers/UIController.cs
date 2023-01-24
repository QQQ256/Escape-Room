using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("����ͼƬ")]
    public Sprite[] onePaperImages; // ������Ҫչʾ�ĵ���ͼֽ
    [Header("�ʼǱ�ͼƬ")]
    public Sprite[] notebookImages;
    [Header("�ʼǱ�UI")]
    public GameObject notebookPanelUI;
    [Header("����ͼUI")]
    public GameObject paperPanelUI;
    [Header("����UI")]
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
    /// ��������ֽҳ��
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
    /// �ʼǱ�ҳ��
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
