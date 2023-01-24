using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Room2Raycast : MonoBehaviour
{
    public Camera cam; 
    [SerializeField]
    private float distance = 3f; 
    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private string orderPwd; // 获取
    private string curPwd;
    private HashSet<string> noteSet; // 一个音符只能点一次，去重
    [SerializeField]
    private List<GameObject> noteList; // 存点过的音符

    void Start()
    {
        //cam = GetComponent<PlayerCam>().cam;
        noteSet = new HashSet<string>();
        orderPwd = GameObject.Find("Gameroot/PuzzleController/Puzzle_01_Notes").GetComponent<NoteController>().orderPassword;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (Input.GetMouseButtonDown(0))
            {
                // 电子琴解密
                if (hitInfo.transform.CompareTag("Puzzle1"))
                {
                    PuzzleController.instance.ClickNotes(hitInfo, noteSet, noteList, orderPwd);
                }
                // 浇花解密
                else if (hitInfo.transform.CompareTag("Puzzle2"))
                {
                    // 关闭puzzle2的脚本
                    AnimationController.instance.PlayFlowerAnimation();
                    PuzzleController.instance.DisableRoom2Puzzle2();
                    // 花开完，执行FinishPuzzle2();
                    //PuzzleController.instance.FinishPuzzle2();
                }
                else if (hitInfo.transform.CompareTag("Puzzle3"))
                {
                    PuzzleController.instance.EnableRoom2Puzzle3Game();
                    GameController.instance.DisableCameraMovement();
                    GameController.instance.DisableSwitchButton();
                    UIController.instance.passwordUI.SetActive(true);
                }
                // 单张纸提示
                else if (hitInfo.transform.CompareTag("Paper"))
                {
                    // 特例判断，打开笔记本
                    if (hitInfo.transform.name.StartsWith("Notebook"))
                    {
                        UIController.instance.ShowNotebookPanel();
                    }
                    else
                    {
                        UIController.instance.ShowPaperPanel(hitInfo);
                    }
                }
            }
        }
    }
}
