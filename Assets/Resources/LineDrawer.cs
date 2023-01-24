using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.ShaderData;

public class LineDrawer : MonoBehaviour
{
    public GameObject[] pairs;
    public GameObject[] lines;
    public LineRenderer line;
    public Transform pos1;
    public Transform pos2;

    public float raycastDistance;

    public LayerMask layerMask;

    private Camera cam;

    [SerializeField]
    private int clickTime;

    [SerializeField]
    private int[] clickedName;

    private Dictionary<int, int> pairDic;

    [SerializeField]
    private int connectedLines;
    // Start is called before the first frame update
    void Start()
    {
        pairDic = new Dictionary<int, int>()
        {
            { 1, 2 },
            { 2, 1 },

            { 3, 4 },
            { 4, 3 },

            { 5, 6 },
            { 6, 5 },

            { 7, 8 },
            { 8, 7 },

            { 9, 10 },
            { 10, 9 },

            { 11, 12 },
            { 12, 11 },
        };

        clickedName = new int[2];
        cam = this.GetComponent<Camera>();
        line.positionCount = 12;
        clickTime = 0;
        connectedLines = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, raycastDistance, layerMask))
        {
            if (clickTime == 2 && !isPairs())
            {
                clickTime = 0;
                ClearArray();
            }
            else if (isPairs())
            {
                int mid = Mathf.Max(clickedName[0], clickedName[1]) / 2;
                Debug.Log(mid);
                lines[mid].gameObject.SetActive(true);
                clickTime = 0;

                //
            }

            if (Input.GetMouseButtonDown(0))
            {

                clickedName[clickTime] = Int32.Parse(hitInfo.transform.name);
                clickTime++;

                if (isPairs())
                {
                    connectedLines++;
                    if (connectedLines == 6)
                    {
                        // 这里走通关后的所有内容
                        cam.enabled = false;
                        GameController.instance.EnableCameraMovement();
                        GameController.instance.EnableSwitchButton();
                        PuzzleController.instance.EnableNextScene();
                        //
                        Room_01_AnimationController.instance.Play_Hypnotizer_Drawer();
                        Room_01_AnimationController.instance.Play_Hypnotizer_RoundScreen();
                    }
                }
            }
        }

        //Line_01();
        //Line_02();
    }

    public bool isPairs()
    {
        if (clickedName.Length == 2)
        {
            if (pairDic.ContainsKey(clickedName[0]))
            {
                if (clickedName[1] == pairDic[clickedName[0]])
                {
                    Debug.Log("right answer!!!");
                    //ConnectLines();
                    return true;
                }
            }
        }
        Debug.Log("wrong answer!");
        return false;
    }

    private void ClearArray()
    {

        for (int i = 0; i < clickedName.Length; i++)
        {
            clickedName[i] = 0;
        }

        Debug.Log("Clear array");
    }
}
