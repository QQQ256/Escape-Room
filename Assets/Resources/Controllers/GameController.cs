using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Button viewSwitchButton;
    public static GameController instance;
    public Dictionary<string, Sprite> obj_img_dicts = new Dictionary<string, Sprite>();

    [Header("��ȡ������ű�")]
    public GameObject child;

    [SerializeField]
    [Header("����ͷ�ƶ��ٶȲ���")]
    public static readonly float cameraSensitiveValue = 6; // �洢����ͷ�ƶ��ٶȲ���

    public GameObject[] interactableObjs;

    public Camera firstPersonCamera;
    private Camera thirdPersonCamera;
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
    // Start is called before the first frame update
    void Start()
    {
        thirdPersonCamera = Camera.main;
        GameObject player = GameObject.Find("Player");
        child = player.transform.Find("Main Camera").gameObject;

        if (SceneManager.GetActiveScene().name == "Room2")
        {
            InitObjToImg();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    #region public method
    public void DisableCameraMovement()
    {
        Debug.Log("DisableCameraMovement");
        child.GetComponent<FirstPersonCamera>().mouseSensitivity = 0;
    }

    public void EnableCameraMovement()
    {
        Debug.Log("EnableCameraMovement");
        child.GetComponent<FirstPersonCamera>().mouseSensitivity = cameraSensitiveValue;
    }

    public void DisableCurrentCamera()
    {
        child.GetComponent<Camera>().enabled = false;
        firstPersonCamera.GetComponent<Camera>().enabled = false;
        Debug.Log("DisableCurrentCamera");
    }

    public void EnableCurrentCamera()
    {
        child.GetComponent<Camera>().enabled = true;
        firstPersonCamera.GetComponent<Camera>().enabled = true;
        Debug.Log("EnableCurrentCamera");
    }

    /// <summary>
    /// �����л��ӽǰ�ť
    /// </summary>
    public void EnableSwitchButton()
    {
        viewSwitchButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// �ر��л��ӽǰ�ť
    /// </summary>
    public void DisableSwitchButton() 
    {
        viewSwitchButton.gameObject.SetActive(false);
    }
    #endregion

    #region private method

    /// <summary>
    /// �������ʼ����Ӧ��ϵ key - ��Ʒ��val - ������Ʒʱ��Ӧ��ͼƬ
    /// �����е�hard coded������п��Ż���
    /// </summary>
    private void InitObjToImg()
    {
        Sprite[] tempSprite = UIController.instance.onePaperImages;

        int len1 = interactableObjs.Length;
        int len2 = tempSprite.Length;
        if (len1 == len2)
        {
            for (int i = 0; i < len1; i++)
            {
                obj_img_dicts[interactableObjs[i].transform.name] = tempSprite[i];
            }
        }

        foreach (var item in obj_img_dicts)
        {
            Debug.Log(item.Key);
            Debug.Log(item.Value);
        }
    }
    #endregion
    
}
