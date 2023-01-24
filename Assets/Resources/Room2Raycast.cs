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
    private string orderPwd; // ��ȡ
    private string curPwd;
    private HashSet<string> noteSet; // һ������ֻ�ܵ�һ�Σ�ȥ��
    [SerializeField]
    private List<GameObject> noteList; // ����������

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
                // �����ٽ���
                if (hitInfo.transform.CompareTag("Puzzle1"))
                {
                    PuzzleController.instance.ClickNotes(hitInfo, noteSet, noteList, orderPwd);
                }
                // ��������
                else if (hitInfo.transform.CompareTag("Puzzle2"))
                {
                    // �ر�puzzle2�Ľű�
                    AnimationController.instance.PlayFlowerAnimation();
                    PuzzleController.instance.DisableRoom2Puzzle2();
                    // �����ִ꣬��FinishPuzzle2();
                    //PuzzleController.instance.FinishPuzzle2();
                }
                else if (hitInfo.transform.CompareTag("Puzzle3"))
                {
                    PuzzleController.instance.EnableRoom2Puzzle3Game();
                    GameController.instance.DisableCameraMovement();
                    GameController.instance.DisableSwitchButton();
                    UIController.instance.passwordUI.SetActive(true);
                }
                // ����ֽ��ʾ
                else if (hitInfo.transform.CompareTag("Paper"))
                {
                    // �����жϣ��򿪱ʼǱ�
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
